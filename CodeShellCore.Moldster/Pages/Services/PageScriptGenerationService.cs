using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace CodeShellCore.Moldster.Pages.Services
{
    public class PageScriptGenerationService : ScriptGenerationServiceBase, IPageScriptGenerationService
    {
        protected IMoldProvider Molds => Store.GetInstance<IMoldProvider>();
        protected INamingConventionService Names => Store.GetInstance<INamingConventionService>();
        protected IPathsService Paths => Store.GetInstance<IPathsService>();
        protected IConfigUnit Unit => Store.GetInstance<IConfigUnit>();

        public PageScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt
            ) : base(prov, opt)
        {
        }

        public virtual void GenerateComponent(string module, PageRenderDTO viewPath, PageJsonData data)
        {
            PageDTO p = Unit.PageRepository.FindSingleForRendering(d => d.Id == viewPath.Id);
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
                ViewParams = data.ViewParams.ToJson(new Newtonsoft.Json.JsonSerializerSettings { StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml }),
                Sources = data.Sources.ToJsonIndent(),
                CollectionId = p.CollectionId == null ? "null" : "'" + p.CollectionId + "'"
            });

            Utils.CreateFolderForFile(scriptPath);
            File.WriteAllText(scriptPath, script);

            WriteSuccess();
        }

        public virtual void GenerateAppComponent(string mod)
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

            string mainCompBase = Unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == mod);
            string temp = Molds.GetResourceByNameAsString(MoldNames.AppComponent_ts);
            var model = new AppComponentModel
            {
                Name = "AppComponent",
                TemplateName = Names.ApplyConvension("AppComponent", AppParts.Component),
                BaseComponentName = mainCompBase.GetAfterLast("/") + "Base",
                BaseComponentPath = Names.GetBaseComponentFilePath(mainCompBase, true)
            };
            string contents = Writer.FillStringParameters(temp, model);

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);

            WriteSuccess();

        }

        public void MoveScript(MovePageRequest r)
        {
            string fromPath = Path.Combine(Paths.UIRoot, r.TenantCode, "app", r.FromPath + ".ts");
            string toPath = Path.Combine(Paths.UIRoot, r.TenantCode, "app", r.ToPath + ".ts");
            if (File.Exists(fromPath))
            {
                Utils.CreateFolderForFile(toPath);
                File.Move(fromPath, toPath);
            }
        }

        public void DeleteScript(string tenantCode, string fromPath)
        {
            string path = Path.Combine(Paths.UIRoot, tenantCode, "app", fromPath + ".ts");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
