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

namespace CodeShellCore.Moldster.Razor.Internal
{
    public class TemplateProcessingService : ConsoleService, ITemplateProcessingService
    {
        protected readonly IPathsService _paths;
        protected readonly IConfigUnit _unit;
        protected readonly IPageControlDataService _controls;
        protected readonly ITemplateDataService _categories;
        protected readonly IPageParameterDataService _pars;
        protected readonly ILocalizationService _loc;
        protected readonly IViewsService _dbViews;


        public TemplateProcessingService(
            IConfigUnit unit,
            IViewsService views,
            IPathsService paths,
            IPageControlDataService con,
            ITemplateDataService cats,
            IPageParameterDataService pars,
            ILocalizationService loc,
            IOutputWriter wtt) : base(wtt)
        {
            _unit = unit;
            _paths = paths;
            _controls = con;
            _dbViews = views;
            _categories = cats;
            _pars = pars;
            _loc = loc;
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
        public void WriteIdOnTemplate(PageCategory cat)
        {
            var dom = AppDomain.CurrentDomain;

            string viewPath = Path.Combine(_paths.ConfigRoot, "Views", cat.ViewPath + ".cshtml");
            string contents = File.ReadAllText(viewPath);

            string firstFive = contents.Length < 6 ? "" : contents.Substring(0, 5);
            if (firstFive != "@*ID:")
            {
                contents = "@*ID:" + cat.Id + "*@\n" + contents;
                File.WriteAllText(viewPath, contents);
            }
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


        private string GetPage(long id)
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

        private string GetPage(string module, string viewPath)
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

        public bool RenderPage(long id, bool verbose = false)
        {
            using (var x = SW.Measure())
            {
                using (Out.Set(ConsoleColor.Cyan))
                    Out.Write(" Html: ");

                PageDTO p = _unit.PageRepository.FindSingleForRendering(e => e.Id == id);

                string template = GetPage(p.Page.Id);
                if (template == null)
                {
                    WriteFailed(x.Elapsed);
                    return false;
                }

                string path = Path.Combine(_paths.UIRoot, p.TenantCode, "app", p.Page.ViewPath);
                string templatePath = path + ".html";

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

        public virtual void GenerateComponentTemplate(string moduleName, PageRenderDTO dto)
        {
            RenderPage(dto.Id);
        }

        public virtual void GenerateMainComponentTemplate(string moduleCode)
        {
            using (var m = SW.Measure())
            {
                using (Out.Set(ConsoleColor.DarkYellow))
                    Out.Write(" Html: ");
                string baseComponent = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == moduleCode);

                string contents = _dbViews.GetMainComponent(baseComponent);
                string filePath = Path.Combine(_paths.UIRoot, moduleCode, "app", "AppComponent.html");

                Utils.CreateFolderForFile(filePath);
                File.WriteAllText(filePath, contents);
                WriteSuccess(m.Elapsed);
            }

        }

        public void MoveHtmlTemplate(MovePageRequest r)
        {
            string fromPath = Path.Combine(_paths.UIRoot, r.TenantCode, "app", r.FromPath + ".html");
            string toPath = Path.Combine(_paths.UIRoot, r.TenantCode, "app", r.ToPath + ".html");
            if (File.Exists(fromPath))
            {
                Utils.CreateFolderForFile(toPath);
                File.Move(fromPath, toPath);
            }
        }

        public void DeleteHtmlTemplate(string tenantCode, string fromPath)
        {
            string path = Path.Combine(_paths.UIRoot, tenantCode, "app", fromPath + ".html");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
