using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.General.Moldster
{
    public class PageLink
    {
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public string IdExpression { get; set; }
        public string IdProperty { get; set; }
        public PageLink(string id, string def = null, string idExpression = null, string idProperty = null)
        {
            Name = id;
            DefaultValue = def;
            IdExpression = idExpression;
            IdProperty = idProperty;
        }
    }
}
