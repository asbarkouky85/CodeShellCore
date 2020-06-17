using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.General
{
    public interface IGeneralHelper
    {
        IHtmlContent TabTitle(IHtmlHelper helper, string containerId, string activationVariable, string textId,IHtmlContent content, object attr);
        IHtmlContent Button<T>(IHtmlHelper<T> helper, string text, string function, string url, BtnClass btn, string icon, string identifier, IHtmlContent content, string classes, string title, object attr);
    }
}
