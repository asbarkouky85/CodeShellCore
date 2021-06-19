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
        string GetSrcFolderPath(string type, string extension = ".ts", bool keepNameformat = false);
        string GetDomainLazyLoadingRoute(string domain);
        string GetComponentFilePath(string module, string viewFilePath);
        string GetBaseComponentFilePath(string viewFilePath, bool import = false);
        string ApplyConvension(string name, AppParts part);
        string GetComponentSelector(string name);
        string GetModuleFilePath(string tenantCode, string domainName, string parentDomain = null, bool createFolder = true);
        string GetBaseModuleFilePath(bool import);
        string GetComponentImportPath(string path, bool fromDomain = true);
        string GetLocalizationJsonPath(string moduleCode, string type, string loc);
        string GetLocalizationLoaderPath(string moduleCode, string loc);
        string ReverseConvention(string v);
    }
}
