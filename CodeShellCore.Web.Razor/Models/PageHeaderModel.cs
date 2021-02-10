using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class PageHeaderModel
    {
        public IHtmlContent Title { get; set; }
        public IHtmlContent AddButton { get; set; }
        public IHtmlContent EmbeddedAddButton { get; set; }
        public IHtmlContent ToolsSection { get; set; }
        public Dictionary<string, string> BreadCrums { get; set; }
        public bool IsListPage { get; set; }

        public PageHeaderModel()
        {
            BreadCrums = new Dictionary<string, string>();
        }
    }
}
