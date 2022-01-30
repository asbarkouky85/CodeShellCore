using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Models
{
    public class RoutesTsModel
    {
        public string ComponentImports { get; set; }
        public string LocalizationImports { get; set; }
        public string Routes { get; set; }
        public string LocalizationLoaders { get; set; }
        public string Registration { get; set; }
        public string DomainsData { get; set; }
        public string BaseName { get; set; }
        public string DefaultRoute { get; internal set; }
    }
}
