using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.General
{
    public interface IGeneralHelper
    {
        void AddHeaderButton(IHtmlHelper helper, IHtmlContent content = null, string function = null, string url = null, BtnClass btn = BtnClass.Default, string icon = null, string identifier = null, string classes = null, string title = null, object attr = null);
        IHtmlContent TabTitle(IHtmlHelper helper, string containerId, string activationVariable, string textId, IHtmlContent content, object attr);
        IHtmlContent Button(IHtmlHelper helper, string text, string function, string url, BtnClass btn, string icon, string identifier, IHtmlContent content, string classes, string title, object attr);
    }
}
