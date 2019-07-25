using CodeShellCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject.UI.Models
{
    public class ServiceUrls
    {
        public string Admin { get { return Shell.GetConfigAs<string>("Services:Admin"); } }
        public string Auth { get { return Shell.GetConfigAs<string>("Services:Auth"); } }
        public string Maintenance { get { return Shell.GetConfigAs<string>("Services:Maintenance"); } }
        public string WorkForce { get { return Shell.GetConfigAs<string>("Services:WorkForce"); } }
        public string Sales { get { return Shell.GetConfigAs<string>("Services:Sales"); } }
        public string Assets { get { return Shell.GetConfigAs<string>("Services:Assets"); } }
        public string Purchasing { get { return Shell.GetConfigAs<string>("Services:Purchasing"); } }
        public string Report { get { return Shell.GetConfigAs<string>("Services:Report"); } }
        public string Public { get { return Shell.GetConfigAs<string>("Services:Public"); } }
    }
}
