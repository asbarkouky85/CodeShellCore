using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Domains.DataObjects
{
    public class PageCategoryDomainDataObject
    {
        public string DomainName { get; set; }
        public List<PageCategoryDataObject> Components { get; set; } = new List<PageCategoryDataObject>();
    }
}
