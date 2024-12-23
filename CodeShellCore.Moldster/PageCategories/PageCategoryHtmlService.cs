using CodeShellCore.Cli;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryHtmlService : RazorViewsServiceBase, IPageCategoryHtmlService
    {
        protected IPathsService _paths => Store.GetInstance<IPathsService>();
        protected IMoldsterUnit _unit => Store.GetInstance<IMoldsterUnit>();
        protected IPageControlDataService _controls => Store.GetInstance<IPageControlDataService>();
        protected IPageCategoryParameterDomainService _categories => Store.GetInstance<IPageCategoryParameterDomainService>();
        protected IPageParameterDataService _pars => Store.GetInstance<IPageParameterDataService>();
        protected ILocalizationService _loc => Store.GetInstance<ILocalizationService>();
        protected IViewsService _dbViews => Store.GetInstance<IViewsService>();

        public PageCategoryHtmlService(IServiceProvider prov, IOptions<MoldsterModuleOptions> opt, IOutputWriter wtt) : base(prov, opt, wtt)
        {
        }

        public async Task ProcessForTenant(string templatePath, string modCode)
        {
            long tempId = await _unit.PageCategoryRepository.GetSingleValue(d => d.Id, d => d.ViewPath == templatePath);
            long tenantId = await _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            await ProcessForTenant(tempId, tenantId);
        }

        public async Task<bool> CollectTemplateData(long id)
        {
            PageCategory p = await _unit.PageCategoryRepository.FindSingle(id);
            if (p == null)
                throw new Exception("Not Found");

            Out.Write(p.ViewPath);
            GotoColumn(6);
            Out.Write(" View Data: ");
            TemplateDataCollector dto = await GetCollector(p.Id);
            if (dto == null)
            {
                WriteFailed();
                return false;
            }
            WriteSuccess();
            Out.Write(" Controls: ");
            await _controls.UpdateTemplateControls(p, dto.Controls);
            await _controls.DeleteUnusedControls(p, dto.Controls);
            var @params = Store.GetInstance<IObjectMapper>().Map(dto.Parameters, new List<PageCategoryParameter>());
            await _categories.UpdateParameters(p, @params);
            if (!string.IsNullOrEmpty(_paths.LocalizationRoot))
                await _loc.UpdateFiles(dto.Localization);
            WriteSuccess();
            return true;
        }

        public async Task UpdateTemplatePages(long id, long tenantId)
        {
            Out.Write(" Pages: ");
            await _controls.UpdateTemplatePages(id, tenantId);
            await _pars.UpdateTemplatePages(id, tenantId);
            await _pars.UpdateTemplatePagesViewParamsJson(tenantId, id);
            WriteSuccess();
        }

        public async Task<bool> ProcessForTenant(long id, long tenantId)
        {
            using (var x = SW.Measure())
            {
                await CollectTemplateData(id);
                await UpdateTemplatePages(id, tenantId);
                using (Out.Set(ConsoleColor.Cyan))
                {
                    Out.Write(" " + x.Elapsed.TotalSeconds.ToString("F4"));
                }
            }

            return true;
        }

        private async Task<TemplateDataCollector> GetCollector(long id)
        {
            try
            {
                return await _dbViews.GetTemplateData(id);
            }
            catch (CodeShellHttpException ex)
            {
                Handle(ex);
                return null;
            }
            catch (Exception ex)
            {
                WriteException(ex, false);
                return null;
            }

        }
    }
}
