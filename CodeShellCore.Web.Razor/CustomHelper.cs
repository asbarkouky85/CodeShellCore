using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor
{
    public abstract class CustomHelper
    {
        public abstract string EntityTypeId { get; }
    }
    public class CustomHelper<T> : CustomHelper, IHtmlHelper<T>
    {

        IHtmlHelper h;
        ViewDataDictionary<T> dic;
        public override string EntityTypeId { get { return typeof(T).Name; } }
        public CustomHelper(IHtmlHelper helper, string modelName, string formName)
        {
            h = helper;
            dic = new ViewDataDictionary<T>(helper.ViewData);

            this.SetNgModel(modelName);
            this.SetNgForm(formName);
        }

        public ViewDataDictionary<T> ViewData { get { return dic; } }

        public Html5DateRenderingMode Html5DateRenderingMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string IdAttributeDotReplacement { get { return ""; } }

        public IModelMetadataProvider MetadataProvider { get { return h.MetadataProvider; } }

        public dynamic ViewBag { get { return h.ViewBag; } }

        public ViewContext ViewContext { get { return h.ViewContext; } }

        public ITempDataDictionary TempData { get { return h.TempData; } }

        public UrlEncoder UrlEncoder { get { return h.UrlEncoder; } }

        ViewDataDictionary IHtmlHelper.ViewData { get { return h.ViewData; } }

        public IHtmlContent ActionLink(string linkText, string actionName, string controllerName, string protocol, string hostname, string fragment, object routeValues, object htmlAttributes)
        {
            return h.ActionLink(linkText, actionName, controllerName, protocol, hostname, fragment, routeValues, htmlAttributes);
        }

        public IHtmlContent AntiForgeryToken()
        {
            return h.AntiForgeryToken();
        }

        public MvcForm BeginForm(string actionName, string controllerName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public MvcForm BeginRouteForm(string routeName, object routeValues, FormMethod method, bool? antiforgery, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent CheckBox(string expression, bool? isChecked, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent CheckBoxFor(Expression<Func<T, bool>> expression, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Display(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent DisplayFor<TResult>(Expression<Func<T, TResult>> expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public string DisplayName(string expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayNameFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayNameForInnerType<TModelItem, TResult>(Expression<Func<TModelItem, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayText(string expression)
        {
            throw new NotImplementedException();
        }

        public string DisplayTextFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent DropDownList(string expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent DropDownListFor<TResult>(Expression<Func<T, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Editor(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent EditorFor<TResult>(Expression<Func<T, TResult>> expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            throw new NotImplementedException();
        }

        public string Encode(object value)
        {
            throw new NotImplementedException();
        }

        public string Encode(string value)
        {
            throw new NotImplementedException();
        }

        public void EndForm()
        {
            throw new NotImplementedException();
        }

        public string FormatValue(object value, string format)
        {
            throw new NotImplementedException();
        }

        public string GenerateIdFromName(string fullName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : struct
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetEnumSelectList(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Hidden(string expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent HiddenFor<TResult>(Expression<Func<T, TResult>> expression, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string Id(string expression)
        {
            throw new NotImplementedException();
        }

        public string IdFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Label(string expression, string labelText, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent LabelFor<TResult>(Expression<Func<T, TResult>> expression, string labelText, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ListBox(string expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ListBoxFor<TResult>(Expression<Func<T, TResult>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string Name(string expression)
        {
            throw new NotImplementedException();
        }

        public string NameFor<TResult>(Expression<Func<T, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IHtmlContent> PartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            return h.PartialAsync(partialViewName, model, viewData);
        }

        public IHtmlContent Password(string expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent PasswordFor<TResult>(Expression<Func<T, TResult>> expression, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent RadioButton(string expression, object value, bool? isChecked, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent RadioButtonFor<TResult>(Expression<Func<T, TResult>> expression, object value, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent Raw(object value)
        {
            return h.Raw(value);
        }

        public IHtmlContent Raw(string value)
        {
            return h.Raw(value);
        }

        public Task RenderPartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            return h.RenderPartialAsync(partialViewName, model, viewData);
        }

        public IHtmlContent RouteLink(string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextArea(string expression, string value, int rows, int columns, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextAreaFor<TResult>(Expression<Func<T, TResult>> expression, int rows, int columns, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextBox(string expression, object value, string format, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent TextBoxFor<TResult>(Expression<Func<T, TResult>> expression, string format, object htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationMessage(string expression, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationMessageFor<TResult>(Expression<Func<T, TResult>> expression, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public IHtmlContent ValidationSummary(bool excludePropertyErrors, string message, object htmlAttributes, string tag)
        {
            throw new NotImplementedException();
        }

        public string Value(string expression, string format)
        {
            throw new NotImplementedException();
        }

        public string ValueFor<TResult>(Expression<Func<T, TResult>> expression, string format)
        {
            throw new NotImplementedException();
        }
    }
}
