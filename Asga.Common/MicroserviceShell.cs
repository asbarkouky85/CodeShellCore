using Asga.Common;
using CodeShellCore.MQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga
{
    public abstract class MicroserviceShell : AsgaShell
    {
        public MicroserviceShell(IConfiguration config) : base(config) { }

        public virtual BusConfig BusConfig { get { return new BusConfig { EndPointId = ProjectAssembly.GetName().Name }; } }

        public virtual string resourcePrefix { get { return ProjectAssembly.GetName().Name.Replace("Asga.", ""); } }
        public static string ResourcePrefix { get { return ((MicroserviceShell)App)?.resourcePrefix; } }
        
        protected override void OnReady()
        {
            base.OnReady();
            
        }

        public override void Dispose()
        {
            base.Dispose();
            
        }
    }
}
