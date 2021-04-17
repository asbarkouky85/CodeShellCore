using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Models
{
    public class ComponentTsModel
    {
        public string BaseClass { get; set; }
        public string BaseClassLocation { get; set; }
        public string ComponentName { get; set; }
        public string TemplateName { get; set; }
        public long PageId { get; set; }
        public string Selector { get; set; }
        public string Resource { get; set; }
        public string Domain { get; set; }
        public string CollectionId { get; set; }
        public string ViewParams { get; set; }
        public string Sources { get; set; }
    }
}
