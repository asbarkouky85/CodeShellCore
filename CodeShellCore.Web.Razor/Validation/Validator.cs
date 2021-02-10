using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation
{
    public abstract class Validator : IValidator
    {
        protected ILocaleTextProvider TextProvider { get { return RazorConfig.LocaleTextProvider; } }
        public string ColumnId { get; set; }
        public abstract string Attribute { get; }
        public abstract string ValidationMessage { get; }
        public string FormName { get; set; }
        public string FormFieldName { get; set; }
        public string Label { get; set; }

        protected string MakeMessage(string index, string message)
        {
            string prop = FormFieldName != null ? FormFieldName : ColumnId.Replace(".", "_");

            return string.Format(RazorConfig.ErrorMessageTemplate, FormName,  prop, index, message);
        }
    }
}
