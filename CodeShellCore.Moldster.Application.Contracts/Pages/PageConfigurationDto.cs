using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public class PageConfigurationDto
    {
        public string PageIdentifier { get; set; }
        public List<Lister> Sources { get; set; }   
        public string Layout { get; set; }
        public int DefaultAccessibility { get; set; }
        public ViewParams ViewParams{ get; set; }
    }
}
