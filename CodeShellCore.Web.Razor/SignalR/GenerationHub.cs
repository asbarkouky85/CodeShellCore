using CodeShellCore.Moldster.Configurator;
using CodeShellCore.Web.Notifiers;

namespace CodeShellCore.Web.Razor.SignalR
{
    public class GenerationHub : SignalRHub<IOutputMessageSender>
    {
        public override string GetConnectionId()
        {
            return base.GetConnectionId();
        }
    }
}
