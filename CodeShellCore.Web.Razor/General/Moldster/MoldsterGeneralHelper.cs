using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
