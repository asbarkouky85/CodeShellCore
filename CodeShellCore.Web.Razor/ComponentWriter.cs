using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Dynamic;

namespace CodeShellCore.Web.Razor
{
    public class ComponentWriter : IDisposable
    {
        public dynamic _inputModelExtraAttrs = new ExpandoObject();
        public dynamic InputModelExtraAttrs { get { return _inputModelExtraAttrs; } }
        protected ILocaleTextProvider TextProvider { get { return RazorConfig.LocaleTextProvider; } }
        protected IHtmlHelper Helper { get; private set; }
        public NgInput InputModel { get; set; }
        public object Model { get; set; }
        protected IServiceProvider Injector { get { return Helper.ViewContext.HttpContext.RequestServices; } }

        public ComponentWriter(IHtmlHelper helper)
        {
            Helper = helper;
        }

        protected virtual string AddInputControlAttributes()
        {
            return "";
        }



        public virtual IHtmlContent Write(InputControls cont, bool localizable = false)
        {
            return Write("InputControls/" + cont.ToString(), localizable);
        }

        public virtual IHtmlContent Write(string componentName, bool localizable = false)
        {
            if (InputModel != null)
            {
                InputModel.Attributes += AddInputControlAttributes();
                InputModel.Attributes += RazorUtils.ToAttributeString(InputModel.AttributeObject);
                return Helper.Partial(Helper.GetTheme().GetTemplate(componentName), InputModel);
            }
            else if (Model != null)
            {
                return Helper.Partial(Helper.GetTheme().GetTemplate(componentName), InputModel);
            }
            return Helper.Partial(Helper.GetTheme().GetTemplate(componentName));

        }


        public virtual IHtmlContent GetInputControl(string componentName)
        {
            InputModel.Attributes += AddInputControlAttributes();
            InputModel.Attributes += RazorUtils.ToAttributeString(InputModel.AttributeObject);
            InputModel.Attributes += RazorUtils.ToAttributeStringDynamic(InputModelExtraAttrs);
            string template = Helper.GetTheme().GetInputControl(componentName);
            return Helper.Partial(template, InputModel);
        }

        public virtual IHtmlContent GetInputControl(InputControls cont)
        {
            InputModel.Attributes += AddInputControlAttributes();
            InputModel.Attributes += RazorUtils.ToAttributeString(InputModel.AttributeObject);
            InputModel.Attributes += RazorUtils.ToAttributeStringDynamic(InputModelExtraAttrs);
            string template = Helper.GetTheme().GetInputControl(cont);
            return Helper.Partial(template, InputModel);
        }

        public virtual void Dispose()
        {

        }
    }
}
