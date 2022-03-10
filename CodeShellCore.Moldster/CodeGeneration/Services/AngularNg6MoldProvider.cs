using System.Resources;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public class AngularNg6MoldProvider : MoldProvider, IMoldProvider
    {
        ResourceManager _manager;

        protected override ResourceManager ResourceManager => _manager;

        public AngularNg6MoldProvider()
        {
            _manager = Ng6.Properties.Resources.ResourceManager;
        }
    }
}
