using CodeShellCore.CliDispatch;
using CodeShellCore.Modularity;
using CodeShellCore.ToolSet.Download;
using CodeShellCore.ToolSet.Ftp;
using CodeShellCore.ToolSet.Help;
using CodeShellCore.ToolSet.Localization;
using CodeShellCore.ToolSet.Modularity;
using CodeShellCore.ToolSet.Nuget;
using CodeShellCore.ToolSet.Replace;
using CodeShellCore.ToolSet.Sql;
using CodeShellCore.ToolSet.TsProxy;
using CodeShellCore.ToolSet.Versions;
using CodeShellCore.ToolSet.Zip;

namespace CodeShellCore.ToolSet
{
    [DependsOn(typeof(ToolsApplicationModule))]
    public class ToolSetCliModule : CodeShellModule
    {
       
    }
}
