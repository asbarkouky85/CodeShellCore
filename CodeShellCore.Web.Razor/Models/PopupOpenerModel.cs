using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class PopupOpenerModel : LinkModel
    {
        public bool Required { get; set; }
        public string ValidationFunction { get; set; }
        public string FormFieldName { get; set; }
        public IHtmlContent ValidationMessage { get; set; }
    }
}
