using CodeShellCore.Modularity;
using CodeShellCore.Web;
using CodeShellCore;

namespace Example.UI.Legacy
{
    [DependsOn(
        typeof(CodeShellWebModule),
		typeof(CodeShellModule)
        )]
    public class ExampleUiLegacyModule : CodeShellModule
    {
    }
}