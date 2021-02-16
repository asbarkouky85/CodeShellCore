using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Text;
using System;
using System.IO;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Types;
using Microsoft.Extensions.Options;

namespace CodeShellCore.Moldster.Razor.Internal
{
    public class TemplateProcessingService : ConsoleService, ITemplateProcessingService
    {
        InstanceStore<object> Store;

        MoldsterModuleOptions opts;
        protected IPathsService _paths => Store.GetInstance<IPathsService>();
        protected IConfigUnit _unit => Store.GetInstance<IConfigUnit>();
        protected IPageControlDataService _controls => Store.GetInstance<IPageControlDataService>();
        protected ITemplateDataService _categories => Store.GetInstance<ITemplateDataService>();
        protected IPageParameterDataService _pars => Store.GetInstance<IPageParameterDataService>();
        protected ILocalizationService _loc => Store.GetInstance<ILocalizationService>();
        private IUIFileNameService _names => Store.GetInstance<IUIFileNameService>();
        protected IViewsService _dbViews => Store.GetInstance<IViewsService>();


        public TemplateProcessingService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt,
            IOutputWriter wtt) : base(wtt)
        {
            opts = opt.Value;
            Store = new InstanceStore<object>(prov);
        }

        public void GenerateGuidTemplate(string moduleCode)
        {
            using (Out.Set(ConsoleColor.Cyan))
                Out.Write(" Html: ");
            var contents = _dbViews.GetGuide(moduleCode);
            string path = Path.Combine(_paths.UIRoot, moduleCode, "app", "Guide/Guide.html"); ;

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);
            WriteSuccess();
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
            _categories.UpdateParameters(p, dto.Parameters);
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

        private void Handle(CodeShellHttpException ex)
        {
            if (ex.HttpResult != null && ex.HttpResult.ExceptionMessage.TryRead(out HttpResult res))
            {
                using (Out.Set(ConsoleColor.Red))
                {
                    Out.WriteLine();
                    Out.WriteLine("Unable to render page : " + res.Message);
                }
                using (Out.Set(ConsoleColor.DarkRed))
                {
                    Out.WriteLine(res.ExceptionMessage);
                    Out.WriteLine();
                }
            }
            else
            {

                WriteException(ex);
            }

        }


        private RenderedPageResult GetPage(long id)
        {
            try
            {

                return _dbViews.GetPageById(id);
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

        private RenderedPageResult GetPage(string module, string viewPath)
        {
            try
            {

                return _dbViews.GetPage(new PageAcquisitorDTO { ModuleCode = module, ViewPath = viewPath });
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

        protected bool RenderPage(long id, out RenderedPageResult res)
        {

            using (var x = SW.Measure())
            {
                res = null;
                using (Out.Set(ConsoleColor.Cyan))
                    Out.Write(" Html: ");

                PageDTO p = _unit.PageRepository.FindSingleForRendering(e => e.Id == id);
                string templatePath = _names.GetComponentFilePath(p.TenantCode, p.Page.ViewPath) + ".html";
                if (!opts.ReplaceComponentHtml && File.Exists(templatePath))
                {
                    WriteColored("Exists", ConsoleColor.Cyan);
                    return true;
                }
                res = GetPage(p.Page.Id);
                string template = res.TemplateContent;
                if (template == null)
                {
                    WriteFailed(x.Elapsed);
                    return false;
                }

                Utils.CreateFolderForFile(templatePath);
                File.WriteAllText(templatePath, template);

                WriteSuccess(x.Elapsed);
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

        public virtual PageJsonData GenerateComponentTemplate(string moduleName, PageRenderDTO dto)
        {
            if (RenderPage(dto.Id, out RenderedPageResult res))
            {
                return res;
            }
            return null;
        }

        public virtual void GenerateMainComponentTemplate(string moduleCode)
        {
            using (var m = SW.Measure())
            {
                using (Out.Set(ConsoleColor.DarkYellow))
                    Out.Write(" Html: ");
                string filePath = _names.GetComponentFilePath(moduleCode, "app") + ".html";

                if (!opts.ReplaceAppComponentHtml && File.Exists(filePath))
                {
                    GotoColumn(SuccessCol);
                    WriteColored("Exists", ConsoleColor.Cyan);
                    return;
                }

                string baseComponent = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == moduleCode);

                string contents = _dbViews.GetMainComponent(baseComponent);


                Utils.CreateFolderForFile(filePath);
                File.WriteAllText(filePath, contents);
                WriteSuccess(m.Elapsed);
            }

        }

        public void MoveHtmlTemplate(MovePageRequest r)
        {
            string fromPath = _names.GetComponentFilePath(r.TenantCode, r.FromPath) + ".html";
            string toPath = _names.GetComponentFilePath(r.TenantCode, r.ToPath) + ".html";
            if (File.Exists(fromPath))
            {
                Utils.CreateFolderForFile(toPath);
                File.Move(fromPath, toPath);
            }
        }

        public void DeleteHtmlTemplate(string tenantCode, string fromPath)
        {
            string path = _names.GetComponentFilePath(tenantCode, fromPath) + ".html";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
