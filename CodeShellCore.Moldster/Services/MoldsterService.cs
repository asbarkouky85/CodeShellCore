using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
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
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services
{
    public class MoldsterService : StandaloneConsoleService, IMoldsterService
    {
        IMoldsterUnit Unit => GetService<IMoldsterUnit>();
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

        public virtual async Task RenderModuleDefinition(string modCode)
        {
            string st = await Data.GetAppStyle(modCode);

            await RenderMainComponent(modCode);
            await TenantTs.AddAngularJson(modCode);

            await DomainTs.GenerateDomainModule(modCode, "Shared");
            await DomainTs.GenerateRoutes(modCode);
            await TenantTs.GenerateAppModule(modCode);
            await TenantTs.GenerateMainFile(modCode);

            await Localization.GenerateJsonFiles(modCode);

        }

        public virtual async Task RenderMainComponent(string mod)
        {
            Out.Write("Writing Main Component for [" + mod + "] : ");
            await PageHtml.GenerateMainComponentTemplate(mod);
            await PageTs.GenerateAppComponent(mod);
            Out.WriteLine();
        }

        public virtual async Task RenderPage(string moduleName, PageRenderDTO dto)
        {
            Out.Write("Writing Component \"" + dto.ViewPath + "\" : ");
            Out.GotoColumn(9);
            var data = await PageHtml.GenerateComponentTemplate(moduleName, dto);
            await PageTs.GenerateComponent(moduleName, dto, data);

            Out.WriteLine();
        }

        public async Task<SubmitResult> ProcessForPage(long value)
        {
            var p = await Unit.PageRepository.FindSingleAs(d => new { d.PageCategoryId, d.TenantId }, d => d.Id == value);
            if (p != null)
            {
                await CatHtml.ProcessForTenant(p.PageCategoryId.Value, p.TenantId);
            }
            return new SubmitResult();
        }

        #region render domain
        public async Task<SubmitResult> RenderDomainModule(RenderDTO dto)
        {
            Out.WriteLine();
            Out.Write("Rendering Module ");

            using (Out.Set(ConsoleColor.Yellow))
                Out.Write(dto.Mod);

            Out.WriteLine("----------------------------");
            string moduleName = dto.Mod;

            var pages = await Data.GetDomainPagesForRendering(dto.Mod, dto.NameChain, dto.Recursive ?? true);

            foreach (var e in pages)
            {
                await RenderPage(moduleName, e);

            }
            var domToDefine = dto.NameChain.Contains("/") ? dto.NameChain.GetBeforeFirst("/") : dto.Domain;
            await DomainTs.GenerateDomainModule(dto.Mod, domToDefine);
            await DomainTs.GenerateRoutes(dto.Mod);
            await Localization.GenerateJsonFiles(dto.Mod);
            Out.WriteLine();
            return new SubmitResult();
        }

        public virtual async Task RenderDomainModule(string mod, string domain, bool lazy)
        {
            await RenderDomainModule(new RenderDTO { Mod = mod, NameChain = domain, Lazy = lazy });

        }

        public async Task<SubmitResult> RenderAll(string modCode)
        {
            var doms = await Data.GetModuleDomains(modCode);

            foreach (var d in doms)
            {
                await RenderDomainModule(modCode, d.NameChain, true);
            }

            await RenderModuleDefinition(modCode);
            return new SubmitResult();
        }
        #endregion

        public async Task<SyncResult> SyncTenants(long src, long tar)
        {
            var consoleSrv = new ConsoleService(Out);
            using (var s = SW.Measure())
            {
                var syncRes = await Unit.TenantRepository.SyncTenants(src, tar);

                SubmitResult res = await PageParameterSrv.UpdateTemplatePagesViewParamsJson(tar);
                consoleSrv.GotoColumn(consoleSrv.SuccessCol);
                if (res.IsSuccess)
                {
                    consoleSrv.WriteSuccess();
                    Out.Write("Affected : " + res.AffectedRows);
                }
                else
                {
                    consoleSrv.WriteFailed();
                }
                Out.WriteLine();
                return syncRes;
            }
        }


        public virtual async Task ProcessTemplates(string modCode, string domain = null)
        {
            if (domain == null)
                await ProcessAllTemplates(modCode);
            else
                await ProcessDomainTemplates(domain, modCode);
        }

        public async Task ProcessAllTemplates(string modCode)
        {
            long tenantId = await Unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            var lst = await Unit.PageCategoryRepository.GetValues(d => d.Id, d => d.Pages.Any(e => e.TenantId == tenantId));
            foreach (long id in lst)
            {
                await CatHtml.ProcessForTenant(id, tenantId);
                await CatTs.GeneratePageCategory(id);
            }
        }

        public async Task ProcessDomainTemplates(string domain, string modCode)
        {
            long tenantId = await Unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            IEnumerable<long> lst = await Unit.PageCategoryRepository.GetDomainTemplates(domain, tenantId);

            foreach (long id in lst)
            {
                await CatHtml.ProcessForTenant(id, tenantId);
                await CatTs.GeneratePageCategory(id);
            }
        }
    }
}
