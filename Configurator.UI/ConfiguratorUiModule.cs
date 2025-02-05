using CodeShellCore.Modularity;
using CodeShellCore.Web;

namespace Configurator.UI
{
    [DependsOn(
        typeof(CodeShellWebModule)
        )]
    public class ConfiguratorUiModule : CodeShellModule
    {
    }
}