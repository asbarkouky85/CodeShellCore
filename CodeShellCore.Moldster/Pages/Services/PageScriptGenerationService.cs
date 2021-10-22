using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace CodeShellCore.Moldster.Pages.Services
{
    public class PageScriptGenerationService : ScriptGenerationServiceBase, IPageScriptGenerationService
    {
        private IMoldProvider _molds => Store.GetInstance<IMoldProvider>();
        private INamingConventionService _fileNameService => Store.GetInstance<INamingConventionService>();
        private IPathsService _paths => Store.GetInstance<IPathsService>();
        private IConfigUnit _unit => Store.GetInstance<IConfigUnit>();

        public PageScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt,
            IOutputWriter output) : base(prov, opt, output)
        {
        }

        public virtual void GenerateComponent(string module, PageRenderDTO viewPath, PageJsonData data)
        {
            PageDTO p = _unit.PageRepository.FindSingleForRendering(d => d.Id == viewPath.Id);
            string scriptPath = _fileNameService.GetComponentFilePath(p.TenantCode, p.Page.ViewPath) + ".ts";

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
                scriptTemplate = _molds.ComponentMold;
            else
                scriptTemplate = _molds.BasicComponent;

            if (p.BaseViewPath == null)
            {
                WriteException(new Exception("Please process template first!!"), false);
                WriteFailed();
                return;
            }

            string script = Writer.FillStringParameters(scriptTemplate, new ComponentTsModel
            {
                BaseClassLocation = _fileNameService.GetBaseComponentFilePath(p.BaseViewPath, true),
                BaseClass = p.BaseViewPath.GetAfterLast("/") + "Base",
                ComponentName = p.Page.Name,
                TemplateName = _fileNameService.ApplyConvension(p.Page.Name, AppParts.Component),

                Domain = p.DomainName,
                Resource = p.ResourceName,
                Selector = _fileNameService.GetComponentSelector(p.Page.Name),
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
            string path = _fileNameService.GetComponentFilePath(mod, "app") + ".ts";

            using (Out.Set(ConsoleColor.DarkRed))
                Out.Write(" Ts: ");

            if (!Options.ReplaceComponentScripts && File.Exists(path))
            {
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }

            string mainCompBase = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == mod);
            string temp = _molds.MainComponentMold;
            var model = new AppComponentModel
            {
                Name = "AppComponent",
                TemplateName = _fileNameService.ApplyConvension("AppComponent", AppParts.Component),
                BaseComponentName = mainCompBase.GetAfterLast("/") + "Base",
                BaseComponentPath = _fileNameService.GetBaseComponentFilePath(mainCompBase, true)
            };
            string contents = Writer.FillStringParameters(temp, model);

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);

            WriteSuccess();

        }

        public void MoveScript(MovePageRequest r)
        {
            string fromPath = Path.Combine(_paths.UIRoot, r.TenantCode, "app", r.FromPath + ".ts");
            string toPath = Path.Combine(_paths.UIRoot, r.TenantCode, "app", r.ToPath + ".ts");
            if (File.Exists(fromPath))
            {
                Utils.CreateFolderForFile(toPath);
                File.Move(fromPath, toPath);
            }
        }

        public void DeleteScript(string tenantCode, string fromPath)
        {
            string path = Path.Combine(_paths.UIRoot, tenantCode, "app", fromPath + ".ts");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
