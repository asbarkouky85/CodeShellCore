using Microsoft.AspNetCore.Html;

namespace CodeShellCore.Web.Razor.Models
{
    public class ColumnModel
    {
        public IHtmlContent InputControl;
        public IHtmlContent ValidationMessages;
        public bool IsRequired;
        public string Attributes;
        public string Width;
        public string MemberName;
        public bool Sorting;
    }
}
