using CodeShellCore.Modularity;
using CodeShellCore.Web.Razor;

namespace GenerationTest.Api
{
    [DependsOn(
        typeof(CodeShellWebRazorModule)
        )]
    public class GenerationTestApiModule : CodeShellModule
    {
    }
}