using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration.Services
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
