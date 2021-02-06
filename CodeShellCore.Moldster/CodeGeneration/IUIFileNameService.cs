using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IUIFileNameService
    {
        string CoreFolder { get; }
        string BaseFolder { get; }
        string GetHttpServiceFolder(string domainPath = null, bool import = false);
        string GetMainTsPath(string moduleCode);
        string GetDomainLazyLoadingRoute(string domain);
        string GetComponentFilePath(string module, string viewFilePath);
        string GetBaseComponentFilePath(string viewFilePath, bool import = false);
        string ApplyConvension(string name, AppParts part);
        string GetComponentSelector(string name);
        string GetModuleFilePath(string tenantCode, string domainName, string parentDomain = null, bool createFolder = true);
        string GetBaseModuleFilePath(bool import);
        string GetComponentImportPath(string name, string basePath = "./", bool fromDomain = true);
    }
}
