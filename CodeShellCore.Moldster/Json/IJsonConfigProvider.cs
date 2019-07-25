using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Json
{
    public interface IJsonConfigProvider : IServiceBase
    {
        MoldsterData GetData();
        PageConfig GetPageConfig(string modCode, string viewPath);
        NgModule GetNgModule(string st);
        PageTemplateConfig GetTemplate(string viewPath);
        DomainWithPages GetDomainModuleWithPages(string moduleCode, string domain);
    }
}
