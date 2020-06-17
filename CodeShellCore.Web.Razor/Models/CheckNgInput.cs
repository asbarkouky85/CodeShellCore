using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class CheckNgInput : NgInput
    {
        
        public IHtmlContent TrueString { get; set; }
        public IHtmlContent FalseString { get; set; }
        public bool UseIcon { get; set; }
        public bool Enabled { get; set; }
        public string Type { get; set; }
        public string ListItemName { get; set; }

        public CheckNgInput() {
            Type = "checkbox";
        }
    }
}
