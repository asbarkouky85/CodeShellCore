using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Services
{
    public class MoldsterService : StandaloneConsoleService, IMoldsterService
    {
        IConfigUnit Unit => GetService<IConfigUnit>();
        IDataService Data => GetService<IDataService>();
        ILocalizationService Localization => GetService<ILocalizationService>();

        IPageParameterDataService PageParameterSrv => GetService<IPageParameterDataService>();
        IDomainScriptGenerationService DomainTs => GetService<IDomainScriptGenerationService>();
        IPageCategoryScriptGenerationService CatTs => GetService<IPageCategoryScriptGenerationService>();
        ITenantScriptGenerationService TenantTs => GetService<ITenantScriptGenerationService>();
        IPageScriptGenerationService PageTs => GetService<IPageScriptGenerationService>();
        IPageHtmlGenerationService PageHtml => GetService<IPageHtmlGenerationService>();
        IPageCategoryHtmlService CatHtml => GetService<IPageCategoryHtmlService>();

        public MoldsterService(IServiceProvider provider) : base(provider)
        {


        }

        public virtual void RenderModuleDefinition(string modCode)
        {
            string st = Data.GetAppStyle(modCode);

            RenderMainComponent(modCode);
            TenantTs.AddAngularJson(modCode);

            DomainTs.GenerateDomainModule(modCode, "Shared");
            DomainTs.GenerateRoutes(modCode);
            TenantTs.GenerateAppModule(modCode);
            TenantTs.GenerateMainFile(modCode);

            Localization.GenerateJsonFiles(modCode);

        }

        public virtual void RenderMainComponent(string mod)
        {
            Out.Write("Writing Main Component for [" + mod + "] : ");
            PageHtml.GenerateMainComponentTemplate(mod);
            PageTs.GenerateAppComponent(mod);
            Out.WriteLine();
        }

        public virtual void RenderPage(string moduleName, PageRenderDTO dto)
        {
            Out.Write("Writing Component \"" + dto.ViewPath + "\" : ");
            Out.GotoColumn(9);
            var data = PageHtml.GenerateComponentTemplate(moduleName, dto);
            PageTs.GenerateComponent(moduleName, dto, data);

            Out.WriteLine();
        }

        public SubmitResult ProcessForPage(long value)
        {
            var p = Unit.PageRepository.FindSingleAs(d => new { d.PageCategoryId, d.TenantId }, d => d.Id == value);
            if (p != null)
            {
                CatHtml.ProcessForTenant(p.PageCategoryId.Value, p.TenantId);
            }
            return new SubmitResult();
        }

        #region render domain
        public SubmitResult RenderDomainModule(RenderDTO dto)
        {
            Out.WriteLine();
            Out.Write("Rendering Module ");

            using (Out.Set(ConsoleColor.Yellow))
                Out.Write(dto.Mod);

            Out.WriteLine("----------------------------");
            string moduleName = dto.Mod;

            var pages = Data.GetDomainPagesForRendering(dto.Mod, dto.NameChain, dto.Recursive ?? true);

            foreach (var e in pages)
            {
                RenderPage(moduleName, e);

            }
            var domToDefine = dto.NameChain.Contains("/") ? dto.NameChain.GetBeforeFirst("/") : dto.Domain;
            DomainTs.GenerateDomainModule(dto.Mod, domToDefine);
            DomainTs.GenerateRoutes(dto.Mod);
            Localization.GenerateJsonFiles(dto.Mod);
            Out.WriteLine();
            return new SubmitResult();
        }

        public virtual void RenderDomainModule(string mod, string domain, bool lazy)
        {
            RenderDomainModule(new RenderDTO { Mod = mod, NameChain = domain, Lazy = lazy });

        }

        public SubmitResult RenderAll(string modCode)
        {
            var doms = Data.GetModuleDomains(modCode);

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
            var wt = new ConsoleService(Out);
            using (var s = SW.Measure())
            {
                var d = con.SyncTenants(src, tar);
                if (d != null)
                {
                    Out.WriteLine();
                    using (Out.Set(ConsoleColor.DarkCyan))
                    {
                        Out.WriteLine("Synced tenant '" + d.SourceTenant + "' to '" + d.TargetTenant + "'");
                    }
                    Out.WriteLine("------------------------------------");
                    Out.WriteLine();

                    Out.Write("Added Pages : ");

                    Out.GotoColumn(5);
                    Out.WriteLine(d.AddedPages.ToString());

                    Out.Write("Added Controls : ");
                    Out.GotoColumn(5);
                    Out.WriteLine(d.AddedPageControls.ToString());

                    Out.Write("Updated Pages : ");
                    Out.GotoColumn(5);
                    Out.WriteLine(d.UpdatedPages.ToString());

                    Out.Write("Updated Controls : ");
                    Out.GotoColumn(5);
                    Out.WriteLine(d.UpdatedPageControls.ToString());

                    Out.Write("Added Navigation Pages : ");
                    Out.GotoColumn(5);
                    Out.WriteLine(d.NavigationPages.ToString());

                    Out.WriteLine();

                }
                Out.Write("Updating viewparams");
                SubmitResult res = PageParameterSrv.UpdateTemplatePagesViewParamsJson(tar);
                wt.GotoColumn(wt.SuccessCol);
                if (res.IsSuccess)
                {
                    wt.WriteSuccess();
                    Out.Write("Affected : " + res.AffectedRows);
                }
                else
                {
                    wt.WriteFailed();
                }
                Out.WriteLine();
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
            long tenantId = Unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            var lst = Unit.PageCategoryRepository.GetValues(d => d.Id, d => d.Pages.Any(e => e.TenantId == tenantId));
            foreach (long id in lst)
            {
                CatHtml.ProcessForTenant(id, tenantId);
                CatTs.GeneratePageCategory(id);
            }
        }

        public void ProcessDomainTemplates(string domain, string modCode)
        {
            long tenantId = Unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            IEnumerable<long> lst = Unit.PageCategoryRepository.GetDomainTemplates(domain, tenantId);

            foreach (long id in lst)
            {
                CatHtml.ProcessForTenant(id, tenantId);
                CatTs.GeneratePageCategory(id);
            }
        }
    }
}
