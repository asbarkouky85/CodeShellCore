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
        public List<LinkModel> Buttons { get; private set; }
        public Dictionary<string, string> BreadCrums { get; set; }
        public bool IsListPage { get; set; }

        public PageHeaderModel()
        {
            BreadCrums = new Dictionary<string, string>();
            Buttons = new List<LinkModel>();
        }

        public void AddToButtons(IHtmlContent content = null, string function = null, string url = null, BtnClass btn = CodeShellCore.Web.Razor.BtnClass.Default, string icon = null, string identifier = null, string classes = null, string title = null, object attr = null)
        {
            Buttons.Add(LinkModel.Make(content, function, url, btn, icon, identifier, classes, title, attr));
        }
    }
}
