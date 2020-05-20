using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Definitions
{
    public class DomainWithPages
    {
        public string Name { get; set; }
        public IEnumerable<PageConfig> Pages { get; set; }
        public string BaseFolder { get; set; }

        public void SetPagesDomainName()
        {
            foreach (var d in Pages)
                d.DomainName = Name;
        }

        public IEnumerable<PageConfig> PagesFilled
        {
            get
            {
                SetPagesDomainName();
                return Pages;
            }
        }
    }
}
