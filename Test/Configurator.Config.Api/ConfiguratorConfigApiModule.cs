using CodeShellCore;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Tasks;
using CodeShellCore.Web.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Configurator.Config.Api
{
    [DependsOn(
        typeof(CodeShellWebRazorModule)
        )]
    public class ConfiguratorConfigApiModule : CodeShellModule
    {
        public override void OnApplicationStopped()
        {
            AsyncHelper.RunSync(async () =>
            {
                var ser = Shell.RootInjector.GetService<IPreviewService>();
                await ser.StopPreview();
            });
        }
    }
}