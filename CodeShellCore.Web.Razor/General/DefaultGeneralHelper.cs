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
            LinkModel model = new LinkModel
            {
                Text = content != null ? content : (text == null ? null : helper.Word(text)),
                Function = function,
                BtnClassEnum = btn,
                Url = url,
                IconClass = icon,
                Attrs = attr,
                Classes = classes,
                Title = title
            };
            return helper.GetComponent("Buttons/Button", model);
        }

        public virtual IHtmlContent TabTitle(IHtmlHelper helper, string containerId, string activationVariable, string textId, object attr)
        {
            var m = new ContainerModel
            {
                ContainerId = containerId,
                ActivationProperty = activationVariable,
                TitleTextId = textId ?? containerId,
                Attributes = attr
            };
            return helper.GetComponent("Components/TabTitle", m);
        }
    }
}
