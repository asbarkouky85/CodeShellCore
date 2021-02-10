using CodeShellCore.Moldster.Db;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Definitions
{
    public class PageTemplateConfig
    {
        public string Resource { get; set; }
        public string Template { get; set; }
        public string BaseComponent { get; set; }
        public string Domain { get; set; }
        

    }
}
