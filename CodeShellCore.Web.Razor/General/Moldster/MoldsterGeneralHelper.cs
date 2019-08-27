using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Db.Dto;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.General.Moldster
{
    public class MoldsterGeneralHelper : DefaultGeneralHelper
    {
        public override IHtmlContent TabTitle(IHtmlHelper helper, string containerId, string activationVariable, string textId, object attr)
        {
            if (!helper.GetAccessibility(containerId).Read)
                return null;
            return base.TabTitle(helper, containerId, activationVariable, textId, attr);
        }

        public override IHtmlContent Button<T>(IHtmlHelper<T> helper, string text, string function, string url, BtnClass btn, string icon, string identifier, IHtmlContent content, string classes, string title, object attr)
        {
            if (identifier != null)
            {
                helper.AddToViewControls(new ControlDTO
                {
                    Identifier = identifier?.ToLower(),
                    ControlType = "Button"
                });
                if (!helper.GetAccessibility(identifier?.ToLower()).Read)
                    return null;
            }
            return base.Button(helper, text, function, url, btn, icon, identifier, content, classes, title, attr);
        }
    }
}
