using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Containers
{
    public class NgForm : HtmlContainer
    {
        string FormName;
        string ModelName;

        string currentNgForm;
        string currentNgModel;
        protected override bool OpenTagInBaseConstructor => false;
        public NgForm(IHtmlHelper helper, string ngForm, string ngModel = null, object attr = null) : base(helper, ngForm, "form", attr)
        {
            FormName = ngForm;
            ModelName = ngModel??helper.GetModelName();
            WriteOpeningTag(attr);
        }

        protected override void WriteOpeningTag(object attr)
        {
            string attrs = RazorUtils.ToAttributeString(attr);
            Helper.ViewContext.Writer.Write(string.Format("<{0} #{1}=\"ngForm\" {2}>\n", TagName,FormName, attrs));
            if (ModelName != null)
            {
                currentNgModel = Helper.GetModelName();
                Helper.SetNgModel(ModelName);
            }

            currentNgForm = Helper.GetFormName();
            Helper.SetNgForm(FormName);
            
        }

        protected override void WriteClosingTag()
        {
            Helper.SetNgForm(currentNgForm);
            Helper.SetNgModel(currentNgModel);
            base.WriteClosingTag();
        }
    }
}
