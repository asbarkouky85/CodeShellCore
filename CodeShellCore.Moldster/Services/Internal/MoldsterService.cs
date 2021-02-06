using System;
using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Services;
using CodeShellCore.Text;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class MoldsterService : StandAloneService, IMoldsterService
    {
        IConfigUnit unit => GetService<IConfigUnit>();
        IDataService _data => GetService<IDataService>();
        ILocalizationService _loc => GetService<ILocalizationService>();
        IOutputWriter _output => GetService<IOutputWriter>();
        IPageParameterDataService _pages => GetService<IPageParameterDataService>();
        IScriptGenerationService _ts => GetService<IScriptGenerationService>();
        ITemplateProcessingService _html => GetService<ITemplateProcessingService>();

        public MoldsterService(IServiceProvider provider) : base(provider)
        {


        }

        public virtual void RenderModuleDefinition(string modCode)
        {
            string st = _data.GetAppStyle(modCode);

            RenderMainComponent(modCode);

            _ts.GenerateDomainModule(modCode, "Shared");
            _ts.GenerateRoutes(modCode);
            _ts.GenerateAppModule(modCode);
            _ts.GenerateMainFile(modCode);
            

            _loc.GenerateJsonFiles(modCode);

        }



        public virtual void RenderMainComponent(string mod)
        {
            _output.Write("Writing Main Component for [" + mod + "] : ");
            _html.GenerateMainComponentTemplate(mod);
            _ts.GenerateAppComponent(mod);
            _output.WriteLine();
        }

        public virtual void RenderPage(string moduleName, PageRenderDTO dto)
        {
            _output.Write("Writing Component \"" + dto.ViewPath + "\" : ");
            _output.GotoColumn(9);
            _html.GenerateComponentTemplate(moduleName, dto);
            _ts.GenerateComponent(moduleName, dto);

            _output.WriteLine();
        }

        public SubmitResult ProcessForPage(long value)
        {
            var p = unit.PageRepository.FindSingleAs(d => new { d.PageCategoryId, d.TenantId }, d => d.Id == value);
            if (p != null)
            {
                _html.ProcessForTenant(p.PageCategoryId.Value, p.TenantId);
            }
            return new SubmitResult();
        }

        #region render domain
        public SubmitResult RenderDomainModule(RenderDTO dto)
        {
            _output.WriteLine();
            _output.Write("Rendering Module ");

            using (_output.Set(ConsoleColor.Yellow))
                _output.Write(dto.Mod);

            _output.WriteLine("----------------------------");
            string moduleName = dto.Mod;

            var pages = _data.GetDomainPagesForRendering(dto.Mod, dto.NameChain, dto.Recursive ?? true);

            foreach (var e in pages)
            {
                RenderPage(moduleName, e);

            }
            var domToDefine = dto.NameChain.Contains("/") ? dto.NameChain.GetBeforeFirst("/") : dto.Domain;
            _ts.GenerateDomainModule(dto.Mod, domToDefine);
            _ts.GenerateRoutes(dto.Mod);
            _loc.GenerateJsonFiles(dto.Mod);
            _output.WriteLine();
            return new SubmitResult();
        }

        public virtual void RenderDomainModule(string mod, string domain, bool lazy)
        {
            RenderDomainModule(new RenderDTO { Mod = mod, NameChain = domain, Lazy = lazy });

        }

        public SubmitResult RenderAll(string modCode)
        {
            var doms = _data.GetModuleDomains(modCode);

            foreach (var d in doms)
            {
                RenderDomainModule(modCode, d.NameChain, true);
            }

            RenderModuleDefinition(modCode);
            return new SubmitResult();
        }
        #endregion

        public SyncResult SyncTenants(long src, long tar)
        {
            var con = GetService<MoldsterContext>();
            var wt = new ConsoleService(_output);
            using (var s = SW.Measure())
            {
                var d = con.SyncTenants(src, tar);
                if (d != null)
                {
                    _output.WriteLine();
                    using (_output.Set(ConsoleColor.DarkCyan))
                    {
                        _output.WriteLine("Synced tenant '" + d.SourceTenant + "' to '" + d.TargetTenant + "'");
                    }
                    _output.WriteLine("------------------------------------");
                    _output.WriteLine();

                    _output.Write("Added Pages : ");

                    _output.GotoColumn(5);
                    _output.WriteLine(d.AddedPages.ToString());

                    _output.Write("Added Controls : ");
                    _output.GotoColumn(5);
                    _output.WriteLine(d.AddedPageControls.ToString());

                    _output.Write("Updated Pages : ");
                    _output.GotoColumn(5);
                    _output.WriteLine(d.UpdatedPages.ToString());

                    _output.Write("Updated Controls : ");
                    _output.GotoColumn(5);
                    _output.WriteLine(d.UpdatedPageControls.ToString());

                    _output.Write("Added Navigation Pages : ");
                    _output.GotoColumn(5);
                    _output.WriteLine(d.NavigationPages.ToString());

                    _output.WriteLine();

                }
                _output.Write("Updating viewparams");
                SubmitResult res = _pages.UpdateTemplatePagesViewParamsJson(tar);
                wt.GotoColumn(wt.SuccessCol);
                if (res.IsSuccess)
                {
                    wt.WriteSuccess();
                    _output.Write("Affected : " + res.AffectedRows);
                }
                else
                {
                    wt.WriteFailed();
                }
                _output.WriteLine();
                return d;
            }


        }


        public virtual void ProcessTemplates(string modCode, string domain = null)
        {
            if (domain == null)
                ProcessAllTemplates(modCode);
            else
                ProcessDomainTemplates(domain, modCode);
        }

        public void ProcessAllTemplates(string modCode)
        {
            long tenantId = unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            var lst = unit.PageCategoryRepository.GetValues(d => d.Id, d => d.Pages.Any(e => e.TenantId == tenantId));
            foreach (long id in lst)
            {
                _html.ProcessForTenant(id, tenantId);
                _ts.GeneratePageCategory(id);
            }
        }

        public void ProcessDomainTemplates(string domain, string modCode)
        {
            long tenantId = unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            IEnumerable<long> lst = unit.PageCategoryRepository.GetDomainTemplates(domain, tenantId);

            foreach (long id in lst)
            {
                _html.ProcessForTenant(id, tenantId);
                _ts.GeneratePageCategory(id);
            }
        }
    }
}
