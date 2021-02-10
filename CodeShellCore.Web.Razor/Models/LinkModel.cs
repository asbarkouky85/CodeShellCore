using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class LinkModel
    {
        public string GroupName { get; set; }
        public string Url { get; set; }
        public IHtmlContent Text { get; set; }
        public string Function { get; set; }
        public BtnClass BtnClassEnum { get; set; }
        public string IconClass { get; set; }
        public string Classes { get; set; }
        public string Content { get; set; }
        public string Attributes { get { return RazorUtils.ToAttributeString(Attrs); } }
        public object Attrs { get; set; }
        public string BtnClass
        {
            get { return RazorUtils.GetButtonClass(BtnClassEnum); }
        }
        public string Title { get; set; }
        public string Icon
        {
            get
            {
                return IconClass == null ? "" : $"<i class=\"{IconClass}\"></i> ";
            }
        }

        public static LinkModel ModifierBtn(string text, string icon, string function, object attrs = null)
        {
            return new LinkModel
            {
                Title = text,
                Attrs = attrs,
                IconClass = icon,
                Function = function,
                BtnClassEnum = Razor.BtnClass.Info
            };
        }
    }
}
