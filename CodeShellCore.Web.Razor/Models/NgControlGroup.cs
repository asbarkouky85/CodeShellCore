using Microsoft.AspNetCore.Html;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Models
{
    public class NgControlGroup
    {
        public string Name { get; set; }
        public string PropertyName { get; set; }
        public string GroupCssClass { get; set; }
        public bool IsRequired { get; set; }
        public bool IsTextArea { get; set; }
        public string RequiredCondition { get; set; }
        public string Label { get; set; }
        public string PlaceHolder { get; set; }
        public string Attributes { get; set; }
        public IHtmlContent InputControl { get; set; }
        public IHtmlContent ValidationMessages { get; set; }
        public int Size { get; set; }
        
        public NgControlGroup()
        {
            Size = RazorConfig.Theme.DefaultControlGroupSize;
        }

        public static NgControlGroup FromExpression<T, TValue>(Expression<Func<T, TValue>> exp)
        {
            string columnId = RazorUtils.GetColumnId(exp);
            return new NgControlGroup
            {
                Label = RazorConfig.LocaleTextProvider.Column(columnId)
            };
        }
    }
}
