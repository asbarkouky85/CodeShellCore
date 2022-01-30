using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Models
{
    public class DomainTsModel
    {
        public string Name { get; set; }
        public string ComponentImports { get; set; }
        public string Components { get; set; }
        public string ParentModules { get; set; }
        public string EmbeddedComponents { get; set; }
        public string Registrations { get; set; }
        public string Routes { get; set; }
        public string Lazy { get; set; }
        public string BaseName { get; set; }
        public string BaseAppModuleName { get; set; }
        public string BaseAppModulePath { get; set; }
        public string PathToRoot { get; set; }
    }
}
