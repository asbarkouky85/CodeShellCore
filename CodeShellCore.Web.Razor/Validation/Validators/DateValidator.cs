using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class DateValidator : Validator
    {
        protected CalendarTypes Type;

        string Pattern;
        protected DateRange Range;
        public DateValidator(string pattern, CalendarTypes type)
        {
            Type = type;
            Pattern = pattern;
        }

        public DateValidator(string pattern, DateRange range)
        {
            Type = CalendarTypes.Custom;
            Pattern = pattern;
            Range = range;
        }
        public override string Attribute
        {
            get
            {
                string attrs = Pattern != null ? "ng-pattern='/" + Pattern + "/'" : "";

                switch (Type)
                {
                    case CalendarTypes.PastDate:
                        attrs += " data-range='past'";
                        break;
                    case CalendarTypes.Custom:
                        attrs += " data-range='" + Range.ToString() + "'";
                        break;
                }
                return attrs;
            }
        }

        public override string ValidationMessage
        {
            get
            {
                List<string> lst = new List<string>();
                string lab = Label;
                if (Pattern != null)
                    lst.Add(MakeMessage("pattern", TextProvider.Message(MessageIds.invalid_field, lab)));

                switch (Type)
                {

                    case CalendarTypes.PastDate:
                        lst.Add(MakeMessage("date_validation", TextProvider.Message(MessageIds.past_date_only, lab)));
                        break;
                    case CalendarTypes.FutureDate:
                        lst.Add(MakeMessage("date_validation", TextProvider.Message(MessageIds.future_date_only, lab)));
                        break;
                    case CalendarTypes.Custom:
                        string message = "";
                        if (Range.Message != null)
                        {
                            message = MakeMessage("date_validation", TextProvider.Message(Range.Message));
                        }
                        else if (!string.IsNullOrEmpty(Range.StartDate) && !string.IsNullOrEmpty(Range.EndDate))
                        {
                            message = MakeMessage("date_validation", TextProvider.Message(MessageIds.invalid_min_and_max, lab, Range.StartDate, Range.EndDate));
                        }
                        else if (!string.IsNullOrEmpty(Range.StartDate))
                        {
                            message = MakeMessage("date_validation", TextProvider.Message(MessageIds.invalid_min, lab, Range.StartDate));
                        }
                        else if (!string.IsNullOrEmpty(Range.EndDate))
                        {
                            message = MakeMessage("date_validation", TextProvider.Message(MessageIds.invalid_max, lab, Range.EndDate));
                        }

                        lst.Add(message);
                        break;
                }

                return string.Join("", lst);
            }
        }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            List<ValidatorModel> lst = new List<ValidatorModel>();
            string lab = Label;
            if (Pattern != null)
                lst.Add(MakeModel("pattern", TextProvider.Message(MessageIds.invalid_field, lab)));

            switch (Type)
            {

                case CalendarTypes.PastDate:
                    lst.Add(MakeModel("date_validation", TextProvider.Message(MessageIds.past_date_only, lab)));
                    break;
                case CalendarTypes.FutureDate:
                    lst.Add(MakeModel("date_validation", TextProvider.Message(MessageIds.future_date_only, lab)));
                    break;
                case CalendarTypes.Custom:
                    var message = new ValidatorModel();
                    if (Range.Message != null)
                    {
                        message = MakeModel("date_validation", TextProvider.Message(Range.Message));
                    }
                    else if (!string.IsNullOrEmpty(Range.StartDate) && !string.IsNullOrEmpty(Range.EndDate))
                    {
                        message = MakeModel("date_validation", TextProvider.Message(MessageIds.invalid_min_and_max, lab, Range.StartDate, Range.EndDate));
                    }
                    else if (!string.IsNullOrEmpty(Range.StartDate))
                    {
                        message = MakeModel("date_validation", TextProvider.Message(MessageIds.invalid_min, lab, Range.StartDate));
                    }
                    else if (!string.IsNullOrEmpty(Range.EndDate))
                    {
                        message = MakeModel("date_validation", TextProvider.Message(MessageIds.invalid_max, lab, Range.EndDate));
                    }

                    lst.Add(message);
                    break;
            }
            return lst;
        }
    }
}
