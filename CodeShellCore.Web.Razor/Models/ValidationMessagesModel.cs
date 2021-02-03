using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class Errors
    {
        public string ErrorType { get; set; }
        public string Message { get; set; }
    }
    public class ValidationMessagesModel
    {
        public string FormName { get; set; }
        public string FieldName { get; set; }
        public IHtmlContent Messages { get; set; }
        public List<ValidatorModel> List { get; set; }
    }
}
