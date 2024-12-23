using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Views;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public class PageHtmlGenerationService : RazorViewsServiceBase, IPageHtmlGenerationService
    {
        protected IPathsService _paths => Store.GetInstance<IPathsService>();
        protected IMoldsterUnit _unit => Store.GetInstance<IMoldsterUnit>();
        private INamingConventionService _names => Store.GetInstance<INamingConventionService>();
        protected IViewsService _dbViews => Store.GetInstance<IViewsService>();

        public PageHtmlGenerationService(IServiceProvider prov, IOptions<MoldsterModuleOptions> opt, IOutputWriter wtt) : base(prov, opt, wtt)
        {
        }

        private async Task<RenderedPageResultDto> GeneratePageHtml(long id)
        {
            try
            {

                return await _dbViews.GetPageById(id);
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

        private Task<RenderedPageResultDto> GetPage(string module, string viewPath)
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

        protected bool RenderPage(long id, out RenderedPageResultDto res)
        {

            using (var x = SW.Measure())
            {
                res = null;
                using (Out.Set(ConsoleColor.Cyan))
                    Out.Write(" Html: ");

                var tsk = _unit.PageRepository.FindSingleAndMap<PageDetailsDto>(e => e.Id == id);
                tsk.Wait();
                PageDetailsDto p = tsk.Result;
                string templatePath = _names.GetComponentFilePath(p.TenantCode, p.Page.ViewPath) + ".html";
                if (!opts.ReplaceComponentHtml && File.Exists(templatePath))
                {
                    WriteColored("Exists", ConsoleColor.Cyan);
                    return true;
                }
                var generateTask = GeneratePageHtml(p.Page.Id);
                res = generateTask.Result;
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



        public virtual Task<PageJsonData> GenerateComponentTemplate(string moduleName, PageRenderDTO dto)
        {
            return Task.Run(() =>
            {
                if (RenderPage(dto.Id, out RenderedPageResultDto res))
                {
                    return (PageJsonData)res;
                }
                return null;
            });
        }

        public virtual async Task GenerateMainComponentTemplate(string moduleCode)
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

                string baseComponent = await _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == moduleCode);

                string contents = await _dbViews.GetMainComponent(baseComponent);


                Utils.CreateFolderForFile(filePath);
                File.WriteAllText(filePath, contents);
                WriteSuccess(m.Elapsed);
            }

        }

        public Task MoveHtmlTemplate(MovePageRequest r)
        {
            return Task.Run(() =>
            {
                string fromPath = _names.GetComponentFilePath(r.TenantCode, r.FromPath) + ".html";
                string toPath = _names.GetComponentFilePath(r.TenantCode, r.ToPath) + ".html";
                if (File.Exists(fromPath))
                {
                    Utils.CreateFolderForFile(toPath);
                    File.Move(fromPath, toPath);
                }
            });
        }

        public Task DeleteHtmlTemplate(string tenantCode, string fromPath)
        {
            return Task.Run(() =>
            {
                string path = _names.GetComponentFilePath(tenantCode, fromPath) + ".html";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            });
        }
    }
}
