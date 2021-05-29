using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.General
{
    public class DefaultGeneralHelper : IGeneralHelper
    {
        public virtual void AddHeaderButton(IHtmlHelper helper, IHtmlContent content = null, string function = null, string url = null, BtnClass btn = BtnClass.Default, string icon = null, string identifier = null, string classes = null, string title = null, object attr = null)
        {
            if (url != null)
                url = helper.GetService<IUIFileNameService>().ApplyConvension(url, AppParts.Route);
            helper.HeaderModel().AddToButtons(content, function, url, btn, icon, identifier, classes, title, attr);
        }

        public virtual IHtmlContent Button(IHtmlHelper helper, string text, string function, string url, BtnClass btn, string icon, string identifier, IHtmlContent content, string classes, string title, object attr)
        {
            content = content != null ? content : (text == null ? null : helper.Word(text));
            if (url != null)
                url = RazorUtils.ApplyConvension(url, AppParts.Route);
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
