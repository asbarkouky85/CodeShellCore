using System;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

using CodeShellCore.Text;

using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web;
using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Web.Razor.Validation.Internal;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Localization;

namespace CodeShellCore.Web.Razor.Elements
{
    public class ControlGroupWriter : ComponentWriter
    {
        protected IValidationCollection VCollection { get; set; }
        protected MemberExpression MemberExpression { get; set; }

        public string ColumnId { get; set; }
        public NgControlGroup GroupModel { get; set; }
        public ControlGroupWriter(IHtmlHelper helper) : base(helper) { }

        protected override string AddInputControlAttributes()
        {
            if (VCollection != null && Accessibility.Write)
                return VCollection.GetAttributes();
            return "";
        }

        public virtual void UseProperty(string memberName, string columnId, string propertyName, bool useGroup = true)
        {
            ColumnId = columnId;
            string groupName = useGroup ? "FG_" + memberName.Replace(".", "_") : null;

            InputModel = new NgInput
            {
                MemberName = memberName,
                NgModelName = Helper.GetModelName(),
                NgFormName = Helper.GetFormName(),
                EntityPropertyName = propertyName,
                GroupName = groupName
            };

            GroupModel = new NgControlGroup
            {
                Label = TextProvider.Column(ColumnId),
                Name = groupName,
                PropertyName = InputModel.MemberName.GetAfterLast(".").UCFirst()
            };

            Helper.AddText(StringType.Column, columnId);
        }

        public override void UseExpression<T, TValue>(Expression<Func<T, TValue>> exp, bool useBsGroup = true)
        {
            ColumnId = RazorUtils.GetColumnId(exp);
            MemberExpression = RazorUtils.GetMemberExpression(exp);

            string groupName = "FG_" + RazorUtils.GetMemberName(exp).Replace(".", "_");

            InputModel = new NgInput
            {
                MemberName = RazorUtils.GetMemberName(exp),
                NgModelName = Helper.GetModelName(),
                NgFormName = Helper.GetFormName(),
                GroupName = useBsGroup ? groupName : null
            };

            GroupModel = new NgControlGroup
            {
                Label = TextProvider.Column(ColumnId),
                Name = useBsGroup ? groupName : null,
                PropertyName = RazorUtils.GetMemberNameDefault(exp).GetAfterLast(".")
            };

            Helper.AddText(StringType.Column, ColumnId);
        }

        public virtual void UseValidation(IValidationCollection coll, string fieldName = null, string alternateLabel = null)
        {
            VCollection = coll;
            if (VCollection == null)
                VCollection = new ValidationCollection(Helper.GetFormName());

            VCollection.SetMember(ColumnId, fieldName ?? InputModel.FieldName, alternateLabel);

            GroupModel.IsRequired = VCollection.HasRequired();
            if (GroupModel.IsRequired)
                GroupModel.RequiredCondition = VCollection.GetRequiredCondition();
            GroupModel.ValidationMessages = VCollection.GetMessages(Helper);

        }


        public virtual void SetOptions(int size, string alternateLabel = null, string placeHolder = null, object attrs = null, object inputAttr = null, string classes = "", string grClasses = "")
        {
            GroupModel.Label = alternateLabel ?? GroupModel.Label;
            GroupModel.Size = size == -1 ? Helper.GetTheme().DefaultControlGroupSize : size;
            GroupModel.Attributes = RazorUtils.ToAttributeString(attrs);
            GroupModel.GroupCssClass = grClasses;

            InputModel.PlaceHolder = placeHolder ?? GroupModel.Label;
            InputModel.Classes = classes;
            InputModel.AttributeObject = inputAttr;

        }

        public virtual IHtmlContent WriteLabel(bool localizable = false)
        {

            if (!(InputModel is LabelNgInput))
                InputModel = InputModel.GetLabelInput();
            GroupModel.InputControl = GetInputControl(localizable ? InputControls.LocalizableLabel : InputControls.Label);
            return Partial(Helper.GetTheme().GetControlGroupTemplate(InputControls.Label, false), GroupModel);
        }

        public override IHtmlContent Write(string componentName, bool localizable = false)
        {
            if (!Accessibility.Read)
                return null;
            if (!Accessibility.Write)
            {
                InputModel.AttributeObject = null;
                return WriteLabel(localizable);
            }

            string template = Helper.GetTheme().GetControlGroupTemplate(componentName, localizable);
            GroupModel.InputControl = GetInputControl(componentName);
            return Partial(template, GroupModel);
        }

        public override IHtmlContent Write(InputControls cont, bool localizable = false)
        {
            if (!Accessibility.Read)
                return null;
            if (!Accessibility.Write)
            {
                GroupModel.RequiredCondition = null;
                GroupModel.IsRequired = false;
                switch (cont)
                {

                    case InputControls.CheckBox:
                        ((CheckNgInput)InputModel).Enabled = false;
                        break;
                    case InputControls.Radio:
                        ((RadioNgInput)InputModel).Enabled = false;
                        break;
                    case InputControls.CalendarTextBox:
                    case InputControls.DateTimeTextBox:
                        InputModel.MemberName = InputModel.MemberName + " | date :'dd-MM-yyyy'";
                        return WriteLabel();
                    case InputControls.FileTextBox:
                        var mem = InputModel.NgModelName + "." + InputModel.MemberName;
                        var url = mem + ".url" + "?'/'+" + mem + ".url:null";
                        InputModel = InputModel.GetLabelInput(url: url, blank: true);
                        InputModel.MemberName = InputModel.MemberName + ".name";
                        return WriteLabel();
                    default:
                        InputModel.AttributeObject = null;
                        return WriteLabel(localizable);
                }

            }
            string template = Helper.GetTheme().GetControlGroupTemplate(cont, localizable);
            GroupModel.InputControl = GetInputControl(cont);
            return Partial(template, GroupModel);
        }

    }
}
