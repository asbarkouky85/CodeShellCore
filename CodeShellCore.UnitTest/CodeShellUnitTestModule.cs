using CodeShellCore.Modularity;
using CodeShellCore.Web.Razor;

namespace CodeShellCore.UnitTest
{
    [DependsOn(
        typeof(CodeShellWebRazorModule)
        )]
    public class CodeShellUnitTestModule : CodeShellModule
    {
    }
}