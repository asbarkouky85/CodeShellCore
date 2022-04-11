using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Types;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace CodeShellCore.Moldster.Pages
{
    public class PageHtmlGenerationService : RazorViewsServiceBase, IPageHtmlGenerationService
    {
        protected IPathsService _paths => Store.GetInstance<IPathsService>();
        protected IConfigUnit _unit => Store.GetInstance<IConfigUnit>();
        private INamingConventionService _names => Store.GetInstance<INamingConventionService>();
        protected IViewsService _dbViews => Store.GetInstance<IViewsService>();

        public PageHtmlGenerationService(IServiceProvider prov, IOptions<MoldsterModuleOptions> opt, IOutputWriter wtt) : base(prov, opt, wtt)
        {
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

        private RenderedPageResult GeneratePageHtml(long id)
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

                PageDetailsDto p = _unit.PageRepository.FindSingleForRendering(e => e.Id == id);
                string templatePath = _names.GetComponentFilePath(p.TenantCode, p.Page.ViewPath) + ".html";
                if (!opts.ReplaceComponentHtml && File.Exists(templatePath))
                {
                    WriteColored("Exists", ConsoleColor.Cyan);
                    return true;
                }
                res = GeneratePageHtml(p.Page.Id);
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
