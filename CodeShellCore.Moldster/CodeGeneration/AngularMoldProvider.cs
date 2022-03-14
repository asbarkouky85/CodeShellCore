using CodeShellCore.Moldster.CodeGeneration.Services;
using System.Resources;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public class AngularMoldProvider : MoldProvider, IMoldProvider
    {
        ResourceManager _manager;

        protected override ResourceManager ResourceManager => _manager;

        public AngularMoldProvider()
        {
            _manager = Ng11.Properties.Resources.ResourceManager;
        }
    }
}
