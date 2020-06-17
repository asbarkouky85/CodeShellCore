using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.General
{
    public class DefaultGeneralHelper : IGeneralHelper
    {
        public virtual IHtmlContent Button<T>(IHtmlHelper<T> helper, string text, string function, string url, BtnClass btn, string icon, string identifier, IHtmlContent content, string classes, string title, object attr)
        {
            content = content != null ? content : (text == null ? null : helper.Word(text));
            LinkModel model = LinkModel.Make(content, function, url, btn, icon, identifier, classes, title, attr);
            return helper.GetComponent("Buttons/Button", model);
        }

        public virtual IHtmlContent TabTitle(IHtmlHelper helper, string containerId, string activationVariable, string textId, IHtmlContent content, object attr)
        {
            var m = new ContainerModel
            {
                ContainerId = containerId,
                ActivationProperty = activationVariable,
                TitleTextId = textId ?? containerId,
                Attributes = attr,
                TitleContent = content
            };
            return helper.GetComponent("Components/TabTitle", m);
        }
    }
}
