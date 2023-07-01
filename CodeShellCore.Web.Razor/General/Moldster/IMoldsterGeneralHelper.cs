using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.General.Moldster
{
    public interface IMoldsterGeneralHelper
    {
        IHtmlContent ComponentSelectorFromOther(IHtmlHelper helper, string id, string def = null, object attr = null);
        string GetLink<T, TValue>(IHtmlHelper<T> helper, string id, Expression<Func<T, TValue>> idProperty, string def);

        string GetLink(IHtmlHelper helper, PageLink link);
        void AddModal(IHtmlHelper helper, string id, string def);
        void AddParameter(IHtmlHelper helper, string id, string def);
    }
}
