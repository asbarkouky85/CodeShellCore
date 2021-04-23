using CodeShellCore.Helpers;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration.Internal
{
    public class AngularFileNameService : IUIFileNameService
    {
        private readonly IPathsService _paths;


        public AngularFileNameService(IPathsService paths)
        {
            _paths = paths;
            CoreFolder = Path.Combine(_paths.UIRoot, "src/core");
            BaseFolder = Path.Combine(CoreFolder, "base");
        }
        public string CoreFolder { get; protected set; }
        public string BaseFolder { get; protected set; }

        public string GetFolder(string folder)
        {
            return _angular_convesion(folder);
        }
        string _angular_convesion(string path)
        {
            path = path.Replace("\\", "/");
            string[] parts = path.Split("/").Select(e => LangUtils.CamelCaseToWords(e.LCFirst(), "-").ToLower()).ToArray();
            return string.Join("/", parts);
        }

        public string GetComponentFilePath(string module, string viewFilePath)
        {
            string url;
            if (viewFilePath == "app")
            {
                url = Utils.CombineUrl(module, viewFilePath);
            }
            else
            {
                var folder = viewFilePath.GetBeforeLast("/");
                var name = viewFilePath.GetAfterLast("/");
                url = Utils.CombineUrl(module, folder, "components", name);
            }

            return Utils.CombineUrl(_paths.UIRoot, "src", ApplyConvension(url, AppParts.Component));
        }

        public string GetDomainLazyLoadingRoute(string domain)
        {
            var path = ApplyConvension(domain, AppParts.Route);
            return
@"    { 
        path: '" + path + @"', 
        loadChildren: () => import('./" + path + "/" + ApplyConvension(domain, AppParts.Module) + "').then(m => m." + domain + @"Module) 
    }";
        }

        public string GetHttpServiceFolder(string domainPath = null, bool import = false)
        {
            domainPath = domainPath ?? "http";
            if (import)
                return Utils.CombineUrl("@base", _angular_convesion(domainPath), "http");
            return Utils.CombineUrl(BaseFolder, _angular_convesion(domainPath), "http");
        }

        public string GetSrcFolderPath(string fileName, string extension = ".ts", bool keepNameformat = false)
        {
            return Utils.CombineUrl(_paths.UIRoot, "src", (keepNameformat ? fileName : fileName.ToLower()) + extension);
        }

        public string GetBaseComponentFilePath(string viewFilePath, bool import = false)
        {
            var path = ApplyConvension(viewFilePath, AppParts.BaseComponent);
            if (import)
                return Utils.CombineUrl("@base", path);
            return Utils.CombineUrl(BaseFolder, path);
        }

        public string ApplyConvension(string name, AppParts part)
        {
            var res = _angular_convesion(name);
            switch (part)
            {
                case AppParts.Module:
                    res = res.Replace("-module", "");
                    res += ".module";
                    break;
                case AppParts.Component:
                    res = res.Replace("-component", "");
                    res += ".component";
                    break;
                case AppParts.BaseComponent:
                    res = res.Replace("-base", "");
                    res += "-base.component";
                    break;
                case AppParts.Service:
                    res = res.Replace("-service", "");
                    res += ".service";
                    break;
                case AppParts.Route:

                    break;
                default:
                    break;
            }
            return res;
        }

        public string GetComponentSelector(string name)
        {
            return "app-" + _angular_convesion(name);
        }

        public string GetModuleFilePath(string tenantCode, string domainName, string parentDomain = null, bool createFolder = true)
        {
            var path = Utils.CombineUrl("src", tenantCode, (parentDomain != null ? "/" + parentDomain + "/" : "") + (createFolder ? domainName + "/" : "") + domainName);
            path = ApplyConvension(path, AppParts.Module);
            return Utils.CombineUrl(_paths.UIRoot, path);
        }

        public string GetBaseModuleFilePath(bool import)
        {
            var path = ApplyConvension(_paths.CoreAppName + "Base", AppParts.Module);
            if (import)
                return Utils.CombineUrl("@base", path);
            return Utils.CombineUrl(BaseFolder, path);
        }

        public string GetComponentImportPath(string path, bool fromDomain = true)
        {
            if (path != "app")
            {
                var component = path.GetAfterLast("/");
                var domain = fromDomain ? "" : path.GetBeforeLast("/");
                path = Utils.CombineUrl(domain, "components", component);
            }
            return ApplyConvension(Utils.CombineUrl("./", path), AppParts.Component);
        }

        public string GetLocalizationJsonPath(string moduleCode, string type, string loc)
        {
            return Path.Combine(_paths.UIRoot, "src", _angular_convesion(moduleCode), "localization", loc, type + ".json");
        }

        public string GetLocalizationLoaderPath(string moduleCode, string loc)
        {
            return Path.Combine(_paths.UIRoot, "src", _angular_convesion(moduleCode), "localization", loc, "loader.ts");
        }
    }
}
