using CodeShellCore.Web.Razor.Elements;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Razor
{
    public static class RazorExtensions
    {
        public static IHtmlContent HijriCalendarGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            CalendarTypes type = CalendarTypes.PastAndFuture,
            DateRange range = null,
            bool required = false,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var Provider = helper.ViewContext.HttpContext.RequestServices.GetService<IElementsHelper>();
            var elem = Provider.CalendarGroup(helper, exp, type, range, Calendars.Hijri, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.HijriCalendarTextBox);
        }
    }
}
