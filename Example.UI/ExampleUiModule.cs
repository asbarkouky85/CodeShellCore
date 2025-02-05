using CodeShellCore.Modularity;
using CodeShellCore.Web;
using Example.Api;
using Example;

namespace Example.UI
{
    [DependsOn(
        typeof(CodeShellWebModule),
		typeof(ExampleApiModule),
		typeof(ExampleModule)
        )]
    public class ExampleUiModule : CodeShellModule
    {
    }
}