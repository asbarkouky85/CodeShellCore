using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.General
{
    public interface IGeneralHelper
    {
        IHtmlContent TabTitle(IHtmlHelper helper, string containerId, string activationVariable, string textId, object attr);
    }
}
