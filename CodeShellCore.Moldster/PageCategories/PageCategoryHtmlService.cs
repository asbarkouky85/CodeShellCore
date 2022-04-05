using CodeShellCore.Cli;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Localization.Services;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryHtmlService : RazorViewsServiceBase, IPageCategoryHtmlService
    {
        protected IPathsService _paths => Store.GetInstance<IPathsService>();
        protected IConfigUnit _unit => Store.GetInstance<IConfigUnit>();
        protected IPageControlDataService _controls => Store.GetInstance<IPageControlDataService>();
        protected IPageCategoryParameterService _categories => Store.GetInstance<IPageCategoryParameterService>();
        protected IPageParameterDataService _pars => Store.GetInstance<IPageParameterDataService>();
        protected ILocalizationService _loc => Store.GetInstance<ILocalizationService>();
        protected IViewsService _dbViews => Store.GetInstance<IViewsService>();

        public PageCategoryHtmlService(IServiceProvider prov, IOptions<MoldsterModuleOptions> opt, IOutputWriter wtt) : base(prov, opt, wtt)
        {
        }

        public void ProcessForTenant(string templatePath, string modCode)
        {
            long tempId = _unit.PageCategoryRepository.GetSingleValue(d => d.Id, d => d.ViewPath == templatePath);
            long tenantId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            ProcessForTenant(tempId, tenantId);
        }

        public bool CollectTemplateData(long id)
        {
            PageCategory p = _unit.PageCategoryRepository.FindSingle(id);
            if (p == null)
                throw new Exception("Not Found");

            Out.Write(p.ViewPath);
            GotoColumn(6);
            Out.Write(" View Data: ");
            TemplateDataCollector dto = GetCollector(p.Id);
            if (dto == null)
            {
                WriteFailed();
                return false;
            }
            WriteSuccess();
            Out.Write(" Controls: ");
            _controls.UpdateTemplateControls(p, dto.Controls);
            _controls.DeleteUnusedControls(p, dto.Controls);
            var @params = Store.GetInstance<IObjectMapper>().Map(dto.Parameters, new List<PageCategoryParameter>());
            _categories.UpdateParameters(p, @params);
            if (!string.IsNullOrEmpty(_paths.LocalizationRoot))
                _loc.UpdateFiles(dto.Localization);
            WriteSuccess();
            return true;
        }

        public void UpdateTemplatePages(long id, long tenantId)
        {
            Out.Write(" Pages: ");
            _controls.UpdateTemplatePages(id, tenantId);
            _pars.UpdateTemplatePages(id, tenantId);
            _pars.UpdateTemplatePagesViewParamsJson(tenantId, id);
            WriteSuccess();
        }

        public bool ProcessForTenant(long id, long tenantId)
        {
            using (var x = SW.Measure())
            {
                CollectTemplateData(id);
                UpdateTemplatePages(id, tenantId);
                using (Out.Set(ConsoleColor.Cyan))
                {
                    Out.Write(" " + x.Elapsed.TotalSeconds.ToString("F4"));
                }
            }

            return true;
        }

        private TemplateDataCollector GetCollector(long id)
        {
            try
            {
                return _dbViews.GetTemplateData(id);
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
