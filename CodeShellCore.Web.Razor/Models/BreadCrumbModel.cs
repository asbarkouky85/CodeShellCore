using CodeShellCore.Web.Razor.General.Moldster;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
   public class BreadCrumbModel
    {
        public IHtmlContent Title { get; set; }
        public string Link { get; set; }
        
    }
}
