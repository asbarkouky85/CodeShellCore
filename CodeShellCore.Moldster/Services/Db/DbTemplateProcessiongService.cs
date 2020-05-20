using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db.Dto;
using System;
using CodeShellCore.Text;
using System.IO;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Http;
using CodeShellCore.CLI;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Data.Helpers;

namespace CodeShellCore.Moldster.Services.Db
{
    public class DbTemplateProcessingService : TemplateProcessingService, IDbTemplateProcessingService
    {

        readonly IConfigUnit _unit;
        readonly IPageControlDataService _controls;
        readonly ITemplateDataService _categories;
        readonly IPageParameterDataService _pars;
        readonly IDbViewsService _dbViews;
        readonly IOutputWriter _writer;

        public DbTemplateProcessingService(
            IConfigUnit unit,
            IDbViewsService views,
            IPathsService paths,
            IPageControlDataService con,
            ITemplateDataService cats,
            IPageParameterDataService pars,
            IOutputWriter wtt) : base(views, paths, wtt)
        {
            _unit = unit;
            _controls = con;
            _dbViews = views;
            _categories = cats;
            _pars = pars;
            _writer = wtt;
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

            _writer.Write(p.ViewPath);
            GotoColumn(6);
            _writer.Write(" View Data: ");
            TemplateDataCollector dto = GetCollector(p.Id);
            if (dto == null)
            {
                WriteFailed();
                return false;
            }
            WriteSuccess();
            _writer.Write(" Controls: ");
            _controls.UpdateTemplateControls(p, dto.Controls);
            _controls.DeleteUnusedControls(p, dto.Controls);
            _categories.UpdateParameters(p, dto.Parameters);
            WriteSuccess();
            return true;
        }

        public void UpdateTemplatePages(long id, long tenantId)
        {
            _writer.Write(" Pages: ");
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
                using (_writer.Set(ConsoleColor.Cyan))
                {
                    _writer.Write(" " + x.Elapsed.TotalSeconds.ToString("F4"));
                }
            }

            return true;
        }

        private void Handle(CodeShellHttpException ex)
        {
            if (ex.HttpResult != null && ex.HttpResult.ExceptionMessage.TryRead(out HttpResult res))
            {
                using (_writer.Set(ConsoleColor.Red))
                {
                    _writer.WriteLine();
                    _writer.WriteLine("Unable to render page : " + res.Message);
                }
                using (_writer.Set(ConsoleColor.DarkRed))
                {
                    _writer.WriteLine(res.ExceptionMessage);
                    _writer.WriteLine();
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
                using (_writer.Set(ConsoleColor.DarkYellow))
                    _writer.Write(" Html: ");

                PageDTO p = _unit.PageRepository.FindSingleAs(PageDTO.ExpressionForRendering, id);

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

        public override void GenerateComponentTemplate(string moduleName, PageRenderDTO dto)
        {
            RenderPage(dto.Id);
        }

        public override void GenerateMainComponentTemplate(string moduleCode)
        {
            using (var m = SW.Measure())
            {
                using (_writer.Set(ConsoleColor.DarkYellow))
                    _writer.Write(" Html: ");
                string baseComponent = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == moduleCode);

                string contents = _views.GetMainComponent(baseComponent);
                string filePath = Path.Combine(_paths.UIRoot, moduleCode, "app", "AppComponent.html");

                Utils.CreateFolderForFile(filePath);
                File.WriteAllText(filePath, contents);
                WriteSuccess(m.Elapsed);
            }

        }
    }
}
