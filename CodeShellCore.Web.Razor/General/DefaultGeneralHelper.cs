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
