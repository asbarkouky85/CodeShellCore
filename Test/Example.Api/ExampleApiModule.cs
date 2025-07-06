using CodeShellCore.Modularity;
using CodeShellCore.Web;

namespace Example.Api
{
    [DependsOn(
        typeof(CodeShellWebModule)
        )]
    public class ExampleApiModule : CodeShellModule
    {
    }
}