using CodeShellCore.Modularity;
using CodeShellCore.Web.Razor;
using Example;

namespace Example.Config.Api
{
    [DependsOn(
        typeof(CodeShellWebRazorModule),
		typeof(ExampleModule)
        )]
    public class ExampleConfigApiModule : CodeShellModule
    {
    }
}