using CodeShellCore.Moldster.CodeGeneration;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
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
        public List<BreadCrumbModel> BreadCrums { get; set; }
        public bool IsListPage { get; set; }
        public BreadCrumbModel ListBreadCrumb { get; set; }

        public PageHeaderModel()
        {
            BreadCrums = new List<BreadCrumbModel>();
            Buttons = new List<LinkModel>();
        }

        public void AddToButtons(IHtmlContent content = null, string function = null, string url = null, BtnClass btn = BtnClass.Default, string icon = null, string identifier = null, string classes = null, string title = null, object attr = null)
        {
            
            if (url != null)
                url = RazorUtils.ApplyConvension(url, AppParts.Route);
            Buttons.Add(LinkModel.Make(content, function, url, btn, icon, identifier, classes, title, attr));
        }
    }
}
