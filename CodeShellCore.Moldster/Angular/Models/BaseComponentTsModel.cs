using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Models
{
    public class BaseComponentTsModel
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public string ParentPath { get; set; }
        public string Domain { get; set; }
        public string Resource { get; set; }
        public string ServicePath { get; set; }
    }
}
