using CodeShellCore.Modularity;
using CodeShellCore.Moldster;

namespace Example
{
    [DependsOn(
        typeof(CodeShellMoldsterApplicationContractsModule)
        )]
    public class ExampleModule : CodeShellModule
    {
    }
}