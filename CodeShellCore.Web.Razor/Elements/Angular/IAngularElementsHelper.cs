using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using CodeShellCore.Web.Razor.Elements;
using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Elements.Angular
{
    public interface IAngularElementsHelper
    {
        IHtmlContent ValueBinding<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> ex, string pipe);
        IHtmlContent InputControl_Ng<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls controls, string textType, Dictionary<string, object> radioOptions, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, string listItem, string inputClasses);
        IHtmlContent SelectModalButton<T>(IHtmlHelper<T> helper1, string textId, string function, string bind, object attrs, string identifier, string validationFunction, string url, IEnumerable<LinkModel> buttons = null);
    }
}
