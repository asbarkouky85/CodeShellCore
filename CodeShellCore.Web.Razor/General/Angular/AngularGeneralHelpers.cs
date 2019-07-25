using System;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Text;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.Elements.Angular;

namespace CodeShellCore.Web.Razor.General.Angular
{
    public static class AngularGeneralHelpers
    {
        static IAngularElementsHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IAngularElementsHelper>(); } }

        public static IHtmlContent ValueBinding<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> ex, string pipe = null)
        {
            return Provider.ValueBinding(helper, ex, pipe);
        }

        public static NgForm NgForm(this IHtmlHelper helper, string ngForm, string ngModel = null, object attrs = null)
        {
            return new NgForm(helper, ngForm, ngModel, attrs);
        }
        
        public static IHtmlContent CalendarFilter(this IHtmlHelper helper, string filterName, string property)
        {
            FilterModel mod = new FilterModel
            {
                FilterName = filterName,
                Property = property
            };
            return helper.GetComponent("Filters/Calendar", mod);
        }
        
        public static IHtmlContent ComponentSelector(this IHtmlHelper helper, string componentPath, string id)
        {
            string res = "";
            if (!string.IsNullOrEmpty(componentPath))
            {
                componentPath = componentPath.GetAfterLast("/").LCFirst();
                res = $"<{componentPath} #{id} [IsEmbedded]='true'></{componentPath}>";
            }
            return new HtmlString(res);
        }

        
    }
}
