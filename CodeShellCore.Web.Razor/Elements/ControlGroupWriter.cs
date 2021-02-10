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
        }

        public override void UseExpression<T, TValue>(Expression<Func<T, TValue>> exp)
        {
            ColumnId = RazorUtils.GetColumnId(exp);
            MemberExpression = RazorUtils.GetMemberExpression(exp);

            string groupName = "FG_" + RazorUtils.GetMemberName(exp).Replace(".", "_");

            InputModel = new NgInput
            {
                MemberName = RazorUtils.GetMemberName(exp),
                NgModelName = Helper.GetModelName(),
                NgFormName = Helper.GetFormName(),
                GroupName = groupName
            };

            GroupModel = new NgControlGroup
            {
                Label = TextProvider.Column(ColumnId),
                Name = groupName,
                PropertyName = RazorUtils.GetMemberNameDefault(exp).GetAfterLast(".")
            };


        }

        public void UseValidation(IValidationCollection coll, string fieldName = null, string alternateLabel = null)
        {
            VCollection = coll;
            if (VCollection == null)
                VCollection = new ValidationCollection(Helper.GetFormName());

            VCollection.SetMember(ColumnId, fieldName ?? InputModel.FieldName, alternateLabel);

            GroupModel.IsRequired = VCollection.HasRequired();
            if (GroupModel.IsRequired)
                GroupModel.RequiredCondition = VCollection.GetRequiredCondition();
            GroupModel.ValidationMessages = VCollection.GetMessages();

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
            return Helper.Partial(Helper.GetTheme().LabelGroupTemplate, GroupModel);
        }

        public override IHtmlContent Write(string componentName, bool localizable = false)
        {
            if (!Accessibility.Read)
                return null;
            if (!Accessibility.Write)
            {
                InputModel.AttributeObject = null;
                return WriteLabel();
            }

            string template = localizable ? Helper.GetTheme().LocalizableControlGroupTemplate : Helper.GetTheme().ControlGroupTemplate;
            GroupModel.InputControl = GetInputControl(componentName);
            return Helper.Partial(template, GroupModel);
        }

        public override IHtmlContent Write(InputControls cont, bool localizable = false)
        {
            if (!Accessibility.Read)
                return null;
            if (!Accessibility.Write)
            {
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
                    default:
                        InputModel.AttributeObject = null;
                        return WriteLabel();
                }

            }
            string template = localizable ? Helper.GetTheme().LocalizableControlGroupTemplate : Helper.GetTheme().ControlGroupTemplate;
            GroupModel.InputControl = GetInputControl(cont);
            return Helper.Partial(template, GroupModel);
        }

    }
}
