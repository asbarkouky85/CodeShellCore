using CodeShellCore.Moldster.Razor;
using CodeShellCore.Text;
using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Web.Razor.Validation.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Tables
{
    public class CellWriter : ComponentWriter
    {
        public ColumnModel ColumnModel { get; set; }
        public LinkModel LinkModel { get; set; }

        public CellWriter(IHtmlHelper helper) : base(helper)
        {
            InputModel = new NgInput
            {
                NgFormName = helper.GetFormName(),
                NgModelName = helper.GetModelName(),
            };
            ColumnModel = new ColumnModel();
        }

        protected IValidationCollection VCollection { get; set; }
        public string ColumnId { get; set; }
        protected MemberExpression MemberExpression { get; set; }
        protected string ModelName { get; set; }
        protected string MemberName { get; set; }

        public override void UseExpression<T, TValue>(Expression<Func<T, TValue>> exp)
        {
            ColumnId = RazorUtils.GetColumnId(exp);
            MemberName = RazorUtils.GetMemberName(exp);
            MemberExpression = RazorUtils.GetMemberExpression(exp);

            ModelName = Helper.GetModelName();

            ColumnModel = new ColumnModel();
            ColumnModel.MemberName = RazorUtils.GetMemberNameDefault(exp);

            InputModel = new NgInput
            {
                MemberName = MemberName,
                NgModelName = Helper.GetModelName(),
                NgFormName = Helper.GetFormName()
            };
            Helper.AddText(StringType.Column,ColumnId);
        }

        public virtual void Initialize(string placeHolder = null, string width = null, object attrs = null, object inputAttr = null, string classes = "")
        {
            ColumnModel.Attributes = RazorUtils.ToAttributeString(attrs);
            ColumnModel.Width = width == null ? "" : $"style='width:{width}'";

            InputModel.PlaceHolder = placeHolder ?? TextProvider.Column(ColumnId);
            InputModel.Classes = classes;
            InputModel.AttributeObject = inputAttr;
        }

        public void UseValidation(IValidationCollection coll, string fieldName = null, string alternateLabel = null, string rowIndex = null)
        {
            VCollection = coll;
            if (VCollection == null)
                VCollection = new ValidationCollection(Helper.GetFormName());
            string defName = InputModel.NgFormName + "__" + MemberName?.Replace(".", "_");
            fieldName = (fieldName ?? defName) + (rowIndex == null ? "" : $"'+{rowIndex}+'");
            VCollection.SetMember(ColumnId, fieldName, alternateLabel);

            ColumnModel.IsRequired = VCollection.HasRequired();
            ColumnModel.ValidationMessages = GetCellErrors(VCollection);


        }

        public IHtmlContent GetCellErrors(IValidationCollection coll)
        {
            ValidationMessagesModel mod = new ValidationMessagesModel
            {
                FormName = coll.FormName,
                FieldName = coll.FormFieldName,

            };
            string mess = "";
            foreach (Validator v in coll.Validators)
            {
                mess += v.ValidationMessage + "\n";
            }
            mod.Messages = new HtmlString(mess);
            return Helper.GetComponent("TableCells/CellValidation", mod);
        }


        public override IHtmlContent Write(string componentName, bool useLocalization = false)
        {

            if (!Accessibility.Read)
                return null;
            if (!Accessibility.Write)
                return WriteCell(CellTypes.LabelCell);
            if (ColumnModel.InputControl == null)
                ColumnModel.InputControl = GetInputControl(componentName);
            string template = Helper.GetTheme().CellTemplate;
            return Helper.Partial(template, ColumnModel);
        }

        public override IHtmlContent Write(InputControls component, bool useLocalization = false)
        {
            if (!Accessibility.Read)
                return null;

            if (!Accessibility.Write)
            {
                switch (component)
                {

                    case InputControls.CheckBox:
                    case InputControls.CheckBoxCell:
                        ((CheckNgInput)InputModel).Enabled = false;
                        break;
                    case InputControls.Radio:
                        ((RadioNgInput)InputModel).Enabled = false;
                        break;
                    case InputControls.CalendarTextBox:
                        InputModel.MemberName = InputModel.MemberName + " | date :'dd-MM-yyyy'";
                        return WriteCell(CellTypes.LabelCell);
                    case InputControls.DateTimeTextBox:
                        InputModel.MemberName = InputModel.MemberName + " | date :'dd-MM-yyyy hh:mm a'";
                        return WriteCell(CellTypes.LabelCell);
                    default:
                        InputModel.AttributeObject = null;
                        return WriteCell(CellTypes.LabelCell);
                }
                
            }
               
            if (ColumnModel.InputControl == null)
                ColumnModel.InputControl = GetInputControl(component);
            string template = Helper.GetTheme().CellTemplate;
            return Helper.Partial(template, ColumnModel);
        }

        public IHtmlContent WriteHeaderCell()
        {
            if (!Accessibility.Read)
                return null;
            ColumnModel.InputControl = new HtmlString(InputModel.PlaceHolder);
            string template = Helper.GetTheme().HeaderCellTemplate;
            return Helper.Partial(template, ColumnModel);
        }

        public IHtmlContent WriteCell(CellTypes types)
        {
            if (!Accessibility.Read)
                return null;
            ColumnModel.InputControl = Helper.Partial(Helper.GetTheme().GetCell(types), InputModel);
            string template = Helper.GetTheme().CellTemplate;
            return Helper.Partial(template, ColumnModel);
        }

        public override IHtmlContent GetInputControl(string componentName)
        {
            if (LinkModel != null)
            {
                return Helper.GetComponent(componentName, LinkModel);
            }
            else
            {
                return base.GetInputControl(componentName);
            }

        }

        protected override string AddInputControlAttributes()
        {
            if (VCollection != null && Accessibility.Write)
                return VCollection.GetAttributes();
            return "";
        }
    }
}
