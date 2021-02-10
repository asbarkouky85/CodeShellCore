using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Validation.Restrictors;
using CodeShellCore.Web.Razor.Validation.Validators;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Validation.Internal
{
    public class ValidationCollection : IValidationCollection
    {
        public string FormName { get; private set; }
        public string FormFieldName { get; private set; }

        private string AlternateLabel;
        private string ColumnId;

        public List<Validator> Validators { get; private set; }

        public bool IsEmpty { get { return !Validators.Any(); } }

        public ValidationCollection(string formName)
        {
            FormName = formName;

            Validators = new List<Validator>();
        }


        public IValidationCollection Add(IValidator v)
        {
            Validators.Add((Validator)v);
            return this;
        }

        public virtual IValidationCollection AddAlphaNumeric()
        {
            Validators.Add(new RestrictAlphaNumeric());
            return this;
        }

        public virtual IValidationCollection AddArabic()
        {
            Validators.Add(new RestrictArabicValidator());
            return this;
        }

        public virtual IValidationCollection AddCustom(string validationType, string message)
        {
            Validators.Add(new CustomValidator(validationType, message));
            return this;
        }

        public virtual IValidationCollection AddEmail()
        {
            Validators.Add(new EmailValidator());
            return this;
        }

        public virtual IValidationCollection AddLength(int maxLength, int minLenth = 0)
        {
            Validators.Add(new LengthValidator(maxLength, minLenth));
            return this;
        }

        public virtual IValidationCollection AddMinMax(float? min, float max = 0)
        {
            Validators.Add(new MinMaxValidator(min, max));
            return this;
        }

        public virtual IValidationCollection AddMinMax(string min, string max = null)
        {
            return this;
        }

        public virtual IValidationCollection AddNumeric()
        {
            Validators.Add(new NumericTextValidator());

            return this;
        }

        public virtual IValidationCollection AddPattern(string pattern, string message = null)
        {
            Validators.Add(new PatternValidator(pattern, message));
            return this;
        }

        public virtual IValidationCollection AddRequired(string reqCondition = null)
        {
            Validators.Add(new RequiredValidator(reqCondition));
            return this;
        }

        public virtual IValidationCollection AddWebsite()
        {
            Validators.Add(new PatternValidator(@"^([a-zA-Z]{3,9}:(\/){2})?([a-zA-Z0-9\.]*){1}(?::[a-zA-Z0-9]{1,4})?(?:\/[_\,\$#~\?\-\+=&;%@\.\w%]+)*(?:\/)?$"));
            return this;
        }

        public string GetAttributes()
        {
            string attributes = "";
            foreach (Validator v in Validators)
            {
                attributes += v.Attribute + " ";
            }
            return attributes;
        }

        public IHtmlContent GetMessages()
        {
            if (IsEmpty)
                return new HtmlString("");
            string messages = "";
            foreach (Validator v in Validators)
            {
                messages += v.ValidationMessage + "\n";
            }
            string res = string.Format(RazorConfig.FieldErrorMessagesTemplate, FormName, FormFieldName, messages);
            return new HtmlString(res);
        }

        public void SetMember(string columnId, string formFieldName, string alternateLabel = null)
        {
            ColumnId = columnId;
            FormFieldName = formFieldName;
            AlternateLabel = alternateLabel;

            foreach (Validator v in Validators)
            {
                v.FormFieldName = FormFieldName;
                v.FormName = FormName;
                v.Label = alternateLabel ?? RazorConfig.LocaleTextProvider.Column(columnId);
                v.ColumnId = columnId;
            }

        }

        public bool HasRequired()
        {
            return Validators.Any(d => d is RequiredValidator);
        }

        public string GetRequiredCondition()
        {
            var v = Validators.FirstOrDefault(d => d is RequiredValidator);
            if (v == null)
                return null;
            return (v as RequiredValidator).RequiredCondition;
        }


        public void SetNumericDefaults()
        {

            if (Validators.Any(d => d is NumericTextValidator || d is RestrictNumericValidator))
            {
                if (!Validators.Any(d => d is RestrictNumericValidator))
                    Validators.Add(new RestrictNumericValidator());
                if (!Validators.Any(d => d is LengthValidator))
                    AddLength(15);
            }
        }

        public void UseAnnotations(IEnumerable<CustomAttributeData> customAttributes)
        {
            if (!Validators.Any(d => d is LengthValidator))
            {
                CustomAttributeData attr = customAttributes.FirstOrDefault(d => d.AttributeType == typeof(StringLengthAttribute));
                if (attr != null)
                {
                    int len = (int)attr.ConstructorArguments.Select(d => d.Value).FirstOrDefault();
                    Validators.Add(new LengthValidator(len));
                }
            }
        }

        public bool Has<TValidator>() where TValidator : IValidator
        {
            return Validators.Any(d => d is TValidator);
        }

        public virtual IValidationCollection AddDate(CalendarTypes type, DateRange range = null)
        {
            DateValidator v;
            if (range == null)
                v = new DateValidator(RazorConfig.DateValidationPattern, type);
            else
                v = new DateValidator(RazorConfig.DateValidationPattern, range);
            Validators.Add(v);
            return this;
        }

        public virtual IValidationCollection AddUnique(string idExpression, string dataService = "Service")
        {
            return this;
        }

        
    }
}
