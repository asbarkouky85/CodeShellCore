using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public class PageScriptGenerationService : ScriptGenerationServiceBase, IPageScriptGenerationService
    {
        protected IMoldProvider Molds => Store.GetRequiredService<IMoldProvider>();
        protected INamingConventionService Names => Store.GetRequiredService<INamingConventionService>();
        protected IPathsService Paths => Store.GetRequiredService<IPathsService>();
        protected IMoldsterUnit Unit => Store.GetRequiredService<IMoldsterUnit>();

        public PageScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt
            ) : base(prov, opt)
        {
        }

        protected virtual Task<string> GetViewParamsJson(long pageId, ViewParams json)
        {
            return Task.Run(() =>
            {
                var res = json.ToJson(new JsonSerializerSettings
                {
                    StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                    Formatting = Formatting.Indented
                });
                return res;
            });
        }

        public virtual async Task GenerateComponent(string module, PageRenderDTO viewPath, PageJsonData data)
        {
            PageDetailsDto p = await Unit.PageRepository.FindSingleAndMap<PageDetailsDto>(d => d.Id == viewPath.Id);
            string scriptPath = Names.GetComponentFilePath(p.TenantCode, p.Page.ViewPath) + ".ts";

            using (Out.Set(ConsoleColor.DarkRed))
            {
                Out.Write(" Ts: ");
            }


            if (File.Exists(scriptPath) && !Options.ReplaceComponentScripts)
            {
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }


            string scriptTemplate = "";
            if (p.ParentHasResource)
                scriptTemplate = Molds.GetResourceByNameAsString(MoldNames.Component_ts);
            else
                scriptTemplate = Molds.GetResourceByNameAsString(MoldNames.BasicComponent_ts);

            if (p.BaseViewPath == null)
            {
                WriteException(new Exception("Please process template first!!"), false);
                WriteFailed();
                return;
            }

            string script = Writer.FillStringParameters(scriptTemplate, new ComponentTsModel
            {
                BaseClassLocation = Names.GetBaseComponentFilePath(p.BaseViewPath, true),
                BaseClass = p.BaseViewPath.GetAfterLast("/") + "Base",
                ComponentName = p.Page.Name,
                TemplateName = Names.ApplyConvension(p.Page.Name, AppParts.Component),

                Domain = p.DomainName,
                Resource = p.ResourceName,
                Selector = Names.GetComponentSelector(p.Page.Name),
                ViewParams = await GetViewParamsJson(p.Page.Id, data.ViewParams),
                Sources = data.Sources.ToJsonIndent(),
                CollectionId = p.CollectionId == null ? "null" : "'" + p.CollectionId + "'"
            });

            Utils.CreateFolderForFile(scriptPath);
            File.WriteAllText(scriptPath, script);

            WriteSuccess();
        }

        public virtual async Task GenerateAppComponent(string mod)
        {
            string path = Names.GetComponentFilePath(mod, "app") + ".ts";

            using (Out.Set(ConsoleColor.DarkRed))
                Out.Write(" Ts: ");

            if (!Options.ReplaceComponentScripts && File.Exists(path))
            {
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }

            string mainCompBase = Paths.CoreAppName + "App";
            string temp = Molds.GetResourceByNameAsString(MoldNames.AppComponent_ts);
            var model = new AppComponentModel
            {
                Name = "AppComponent",
                TemplateName = Names.ApplyConvension("AppComponent", AppParts.Component),
                BaseComponentName = mainCompBase.GetAfterLast("/") + "BaseComponent",
                BaseComponentPath = Names.GetBaseComponentFilePath(mainCompBase, true)
            };
            string contents = Writer.FillStringParameters(temp, model);

            Utils.CreateFolderForFile(path);
            await File.WriteAllTextAsync(path, contents);

            WriteSuccess();

        }

        public Task MoveScript(MovePageRequest r)
        {
            return Task.Run(() =>
            {

                string fromPath = Path.Combine(Paths.UIRoot, r.TenantCode, "app", r.FromPath + ".ts");
                string toPath = Path.Combine(Paths.UIRoot, r.TenantCode, "app", r.ToPath + ".ts");
                if (File.Exists(fromPath))
                {
                    Utils.CreateFolderForFile(toPath);
                    File.Move(fromPath, toPath);
                }
            });
        }

        public Task DeleteScript(string tenantCode, string fromPath)
        {
            return Task.Run(() =>
            {

                string path = Path.Combine(Paths.UIRoot, tenantCode, "app", fromPath + ".ts");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            });
        }
    }
}
