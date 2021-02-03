
using CodeShellCore.Web.Razor.Validation.Validators;
using CodeShellCore.Web.Razor.Validation.Validators.Angular;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation.Internal
{
    public class AngularValidationCollection : ValidationCollection
    {
        public AngularValidationCollection(string formName) : base(formName)
        {
        }

        public override IValidationCollection AddPattern(string pattern, string message = null)
        {
            Validators.Add(new AngularPatternValidator(pattern, message));
            return this;
        }

        public override IValidationCollection AddRequired(string reqCondition = null)
        {
            Validators.Add(new AngularRequiredValidator(reqCondition));
            return this;
        }

        public override IValidationCollection AddUnique(string idExpression, string dataService = "Service")
        {

            Validators.Add(new UniqueValidator(dataService, idExpression));
            return this;
        }

        public override IValidationCollection AddDate(CalendarTypes type, DateRange range = null)
        {
            AngularDateValidator v;
            if (range == null)
                v = new AngularDateValidator(type);
            else
                v = new AngularDateValidator(range);
            Validators.Add(v);
            return this;
        }

        public override IValidationCollection AddEmail()
        {
            Validators.Add(new AngularPatternValidator("'" + Patterns.Email + "'"));
            return this;
        }

        public override IValidationCollection AddNumeric()
        {
            Validators.Add(new AngularPatternValidator("'" + Patterns.Numeric + "'"));
            return base.AddNumeric();
        }

        public override IValidationCollection AddMinMax(string min, string max = null, string message = null)
        {
            Add(new AngularMinMaxValidator(max, min, message));
            return this;
        }
    }
}
