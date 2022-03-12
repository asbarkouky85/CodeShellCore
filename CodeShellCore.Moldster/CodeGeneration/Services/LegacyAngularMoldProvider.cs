using System.Resources;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public class LegacyAngularMoldProvider : MoldProvider, IMoldProvider
    {
        ResourceManager _manager;

        protected override ResourceManager ResourceManager => _manager;

        public LegacyAngularMoldProvider()
        {
            _manager = Ng6.Properties.Resources.ResourceManager;
        }
    }
}
