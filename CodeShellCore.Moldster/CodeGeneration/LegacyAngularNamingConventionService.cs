using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Text;
using System.IO;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public class LegacyAngularNamingConventionService : INamingConventionService
    {
        private readonly IPathsService _paths;


        public LegacyAngularNamingConventionService(IPathsService paths)
        {
            _paths = paths;
            CoreFolder = Path.Combine(_paths.UIRoot, "Core");
            BaseFolder = Path.Combine(CoreFolder, _paths.CoreAppName);
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
            return path;
        }



        public string GetComponentFilePath(string module, string viewFilePath)
        {
            string url;
            if (viewFilePath == "app")
            {
                url = Utils.CombineUrl(module, "app", "AppComponent");
            }
            else
            {
                var folder = viewFilePath.GetBeforeLast("/");
                var name = viewFilePath.GetAfterLast("/");
                url = Utils.CombineUrl(module, "app", folder, name);
            }

            return Utils.CombineUrl(_paths.UIRoot, ApplyConvension(url, AppParts.Component));
        }

        public string GetDomainLazyLoadingRoute(string domain)
        {
            return "{ path:\"" + domain + "\", loadChildren:\"./" + domain + "/" + domain + "Module#" + domain + "Module\" }";
        }

        public string GetHttpServiceFolder(string domainPath = null, bool import = false)
        {
            if (domainPath == null)
            {
                if (import)
                    return _paths.CoreAppName + "/Http";
                return Utils.CombineUrl(BaseFolder, "Http");
            }
            else
            {
                if (import)
                    return Utils.CombineUrl(_paths.CoreAppName, _angular_convesion(domainPath), "Http");
                return Utils.CombineUrl(BaseFolder, _angular_convesion(domainPath), "Http");
            }

        }

        public string GetSrcFolderPath(string fileName, string extension = ".ts", bool keepNameformat = false)
        {
            return Utils.CombineUrl(_paths.UIRoot, "src", (keepNameformat ? fileName : fileName.ToLower()) + extension);
        }

        public string GetBaseComponentFilePath(string viewFilePath, bool import = false)
        {
            var path = ApplyConvension(viewFilePath, AppParts.BaseComponent);
            if (import)
                return Utils.CombineUrl(_paths.CoreAppName, path);
            return Utils.CombineUrl(BaseFolder, path);
        }

        public string ApplyConvension(string name, AppParts part)
        {
            var res = _angular_convesion(name);

            switch (part)
            {
                case AppParts.Module:
                    res += "Module";
                    break;
                case AppParts.BaseComponent:
                    res += "Base";
                    break;
                case AppParts.Service:
                    res += "Service";
                    break;
                case AppParts.Route:
                    //res += "Module";
                    break;
                case AppParts.Project:
                    //res += "Module";
                    break;
            }
            return res;
        }

        public string GetOutputBundlePath(string tenantCode, string version, bool full = false)
        {
            var projectName = ApplyConvension(tenantCode, AppParts.Project);
            string outFolder = "wwwroot/" + projectName + "_v" + version;
            if (full)
            {
                outFolder = Path.Combine(_paths.UIRoot, outFolder).Replace("\\", "/");
            }
            return outFolder + ".zip";
        }

        public string GetOutputPath(string tenantCode, string version, bool full = false)
        {
            var projectName = ApplyConvension(tenantCode, AppParts.Project);
            string outFolder = "wwwroot/" + projectName;
            if (full)
            {
                outFolder = Path.Combine(_paths.UIRoot, outFolder).Replace("\\", "/");
            }
            return outFolder;
        }

        public string GetComponentSelector(string name)
        {
            return _angular_convesion(name);
        }

        public string GetModuleFilePath(string tenantCode, string domainName, string parentDomain = null, bool createFolder = true)
        {
            var path = Path.Combine(tenantCode, "app\\" + (parentDomain != null ? "\\" + parentDomain + "\\" : "") + domainName + "\\" + domainName);
            path = ApplyConvension(path, AppParts.Module);
            return Utils.CombineUrl(_paths.UIRoot, path);
        }

        public string GetBaseModuleFilePath(bool import)
        {
            var path = ApplyConvension(_paths.CoreAppName + "Base", AppParts.Module);
            if (import)
                return Utils.CombineUrl(_paths.CoreAppName, path);
            return Utils.CombineUrl(BaseFolder, path);
        }

        public string GetComponentImportPath(string path, bool fromDomain = true)
        {
            if (path != "app")
            {
                var component = path.GetAfterLast("/");
                var domain = fromDomain ? "" : path.GetBeforeLast("/");
                path = Utils.CombineUrl(domain, component);
            }
            return ApplyConvension(Utils.CombineUrl("./", path), AppParts.Component);
        }

        public string GetLocalizationJsonPath(string moduleCode, string type, string loc)
        {
            return Path.Combine(_paths.UIRoot, _angular_convesion(moduleCode), "Localization", loc, type + ".json");
        }

        public string GetLocalizationLoaderPath(string moduleCode, string loc)
        {
            return Path.Combine(_paths.UIRoot, _angular_convesion(moduleCode), "Localization", loc, "loader.ts");
        }

        public string ReverseConvention(string v)
        {
            return v;
        }

        public virtual string GetCodeShellBaseComponentsImportPath()
        {
            return "codeshell/baseComponents";
        }

        public string GetLogoFilePath(string tenantCode, string fileName)
        {
            return Path.Combine(_paths.UIRoot, "wwwroot\\logos", fileName);
        }
    }
}
