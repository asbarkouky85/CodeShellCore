using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class AngularDateValidator : DateValidator
    {
        public AngularDateValidator(CalendarTypes types) : base(null, types) { }
        public AngularDateValidator(DateRange range) : base(null, range) { }

        public override string Attribute
        {
            get
            {
                string attrs = "";

                switch (Type)
                {
                    case CalendarTypes.PastDate:
                        attrs += " date-validate=\"past\"";
                        break;
                    case CalendarTypes.FutureDate:
                        attrs += " date-validate=\"future\"";
                        break;
                    case CalendarTypes.Custom:
                        attrs = "date-validate";
                        if (Range.StartDate != null)
                            attrs += " [min-date]=\"" + Range.StartDate + "\"";
                        if (Range.EndDate != null)
                            attrs += " [max-date]=\"" + Range.EndDate + "\"";
                        break;
                }
                return attrs;
            }
        }
    }
}
