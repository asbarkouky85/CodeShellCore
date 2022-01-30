using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ
{
    public class BusConfig
    {
        public virtual string Uri { get { return Shell.GetConfigAs<string>("MQ:Uri"); } }
        public virtual string User { get { return Shell.GetConfigAs<string>("MQ:User"); } }
        public virtual string Password { get { return Shell.GetConfigAs<string>("MQ:Password"); } }

        public string EndPointId { get; set; }

        
        public static BusConfig Current { get; set; }
    }
}
