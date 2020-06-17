using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Models
{
    public class NgInput
    {
        private string _fieldName;
        public string GroupName { get; set; }
        public string NgFormName { get; set; }
        public string NgModelName { get; set; }
        public string AngularJsConfig { get; set; }
        public string MemberName { get; set; }
        public string EntityPropertyName { get; set; } = "Empty";
        public string PlaceHolder { get; set; }
        public string RowIndex { get; set; }
        public string TextBoxType { get; set; }
        public string Classes { get; set; }
        public string CalendarOptions { get; set; }
        public string Attributes { get; set; }
        public object AttributeObject { get; set; }
        public string NgOptions { get; set; }
        public virtual string FieldName
        {
            get { return _fieldName ?? generateFieldName(); }
            set { _fieldName = value; }
        }


        public NgInput()
        {
            Attributes = "";
        }

        private string generateFieldName()
        {
            return NgFormName + "__" + MemberName?.Replace(".", "_") + (RowIndex == null ? "" : "{{" + RowIndex + "}}");
        }

        public FileNgInput GetFileInput(string uploadUrl, string formFieldName, bool multiple = false)
        {
            return new FileNgInput
            {
                MemberName = MemberName,
                NgModelName = NgModelName,
                EntityPropertyName = EntityPropertyName,
                UploadUrl = uploadUrl,
                FormFieldName = formFieldName,
                AttributeObject = AttributeObject,
                Multiple = multiple,
                Classes = Classes
            };
        }

        public SelectNgInput GetSelectInput(string listName, string displayMember, string valueMember = null, bool multi = false, bool nullable = false)
        {
            return new SelectNgInput
            {
                MemberName = MemberName,
                NgModelName = NgModelName,
                ListName = listName,
                Display = displayMember,
                ValueMember = valueMember,
                AttributeObject = AttributeObject,
                EntityPropertyName = EntityPropertyName,
                Classes = Classes,
                Multi = multi,
                Nullable = nullable,
                GroupName = GroupName,
                NgFormName = NgFormName
            };
        }

        public LabelNgInput GetLabelInput(string pipe = null, string url = null, bool blank = true)
        {
            return new LabelNgInput
            {
                MemberName = MemberName,
                NgModelName = NgModelName,
                AttributeObject = AttributeObject,
                EntityPropertyName = EntityPropertyName,
                Classes = Classes,
                Pipe = pipe,
                Url = url,
                Blank = blank
            };
        }



        public RadioNgInput GetRadioInput(Dictionary<string, object> values, bool enabled = true)
        {
            return new RadioNgInput
            {
                MemberName = MemberName,
                NgModelName = NgModelName,
                Values = values,
                EntityPropertyName = EntityPropertyName,
                AttributeObject = AttributeObject,
                Enabled = enabled,
                Classes = Classes,
                GroupName = GroupName
            };
        }

        public CheckNgInput GetCheckInput(IHtmlContent trueString, IHtmlContent falseString, bool useIcon, string listItem = null, string type = "checkbox")
        {
            return new CheckNgInput
            {
                MemberName = MemberName,
                NgModelName = NgModelName,
                TrueString = trueString,
                FalseString = falseString,
                AttributeObject = AttributeObject,
                EntityPropertyName = EntityPropertyName,
                UseIcon = useIcon,
                Classes = Classes,
                Enabled = true,
                ListItemName = listItem,
                Type = type
            };
        }
    }
}
