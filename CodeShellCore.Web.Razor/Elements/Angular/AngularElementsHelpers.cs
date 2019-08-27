using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Elements.Angular
{
    public static class AngularElementsHelpers
    {
        static IAngularElementsHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IAngularElementsHelper>(); } }
        public static IHtmlContent ValueBinding<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> ex, string pipe)
        {
            return Provider.ValueBinding(helper, ex, pipe);
        }

        public static IHtmlContent InputControl_Ng<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls controls, string textType, Dictionary<string, object> radioOptions, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, string listItem, string inputClasses)
        {
            return Provider.InputControl_Ng(helper, exp, controls, textType, radioOptions, coll, size, alternateLabel, placeHolder, attrs, listItem, inputClasses);
        }

    }
}
