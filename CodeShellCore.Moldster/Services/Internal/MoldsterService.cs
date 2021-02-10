using System;
using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services.Db;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class MoldsterService : StandAloneService, IMoldsterService
    {
        IDbTemplateProcessingService _cust => GetService<IDbTemplateProcessingService>();
        IConfigUnit unit => GetService<IConfigUnit>();
        IScriptGenerationService _ts => GetService<IScriptGenerationService>();
        ITemplateProcessingService _html => GetService<ITemplateProcessingService>();
        ILocalizationService _loc => GetService<ILocalizationService>();
        IOutputWriter _output => GetService<IOutputWriter>();

        IDataService _data => GetService<IDataService>();
        IPageParameterDataService _pages => GetService<IPageParameterDataService>();

        public MoldsterService(IServiceProvider provider) : base(provider)
        {


        }

        public virtual void RenderModuleDefinition(string modCode, bool lazy)
        {
            string st = _data.GetAppStyle(modCode);

            RenderMainComponent(modCode);

            _ts.GenerateDomainModule(modCode, "Shared", lazy);
            _ts.GenerateRoutes(modCode, lazy);
            _ts.GenerateModuleDefinition(modCode, lazy);
            if (st != null)
                _ts.GenerateStyle(modCode, st);
            _ts.GenerateBootFile(modCode, st != null);

            _loc.GenerateJsonFiles(modCode);

        }

        public virtual void RenderDomainModule(string mod, string domain, bool lazy)
        {

            _output.WriteLine();
            _output.Write("Rendering Module ");

            using (_output.Set(ConsoleColor.Yellow))
                _output.Write(mod);

            _output.WriteLine("----------------------------");
            string moduleName = mod;

            var pages = _data.GetDomainPagesForRendering(mod, domain);

            foreach (var e in pages)
            {
                RenderPage(moduleName, e);
            }
            _ts.GenerateDomainModule(mod, domain, lazy);
            _loc.GenerateJsonFiles(mod);
            _output.WriteLine();
        }

        public virtual void RenderMainComponent(string mod)
        {
            _output.Write("Writing Main Component for [" + mod + "] : ");
            _html.GenerateMainComponentTemplate(mod);
            _ts.GenerateMainComponent(mod);
            _output.WriteLine();
        }

        public virtual void ProcessTemplates(string modCode, string domain = null)
        {
            string[] tem = _data.GetTemplatePaths(modCode, domain);
            foreach (var t in tem)
            {
                _ts.GenerateBaseComponent(t);
                _output.WriteLine();
            }
        }

        public virtual void RenderPage(string moduleName, PageRenderDTO dto)
        {
            _output.Write("Writing Component \"" + dto.ViewPath + "\" : ");
            _output.GotoColumn(9);
            _html.GenerateComponentTemplate(moduleName, dto);
            _ts.GenerateComponent(moduleName, dto);

            _output.WriteLine();
        }

        public virtual void RenderGuid(string module)
        {
            _output.Write("Writing Page \"Guide/Guide\"");
            _output.GotoColumn(9);
            _html.GenerateGuidTemplate(module);
            _ts.GenerateGuidComponent(module);

            _output.WriteLine();
        }

        public SubmitResult ProcessForPage(long value)
        {
            var p = unit.PageRepository.FindSingleAs(d => new { d.PageCategoryId, d.TenantId }, d => d.Id == value);
            if (p != null)
            {
                _cust.ProcessForTenant(p.PageCategoryId.Value, p.TenantId);
            }
            return new SubmitResult();
        }

        public SubmitResult RenderAll(string modCode)
        {
            var doms = _data.GetModuleDomains(modCode);

            foreach (var d in doms)
            {
                RenderDomainModule(modCode, d.NameChain, true);
            }
            RenderModuleDefinition(modCode, true);
            return new SubmitResult();
        }

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
    }
}
