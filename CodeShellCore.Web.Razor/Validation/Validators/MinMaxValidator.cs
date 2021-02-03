using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class MinMaxValidator : Validator
    {
        protected float? Min;
        protected float Max;
        protected string Message;
        public override string Attribute
        {
            get
            {
                string str = "";
                if (Min.HasValue)
                    str += "min='" + Min + "'";

                if (Max > 0)
                    str += "max='" + Max + "'";

                return str;
            }
        }

        public MinMaxValidator(float? min, float max = 0)
        {
            Min = min;
            Max = max;
        }

        public override string ValidationMessage
        {
            get
            {
                List<string> lst = new List<string>();
                string lab = Label;

                if (Min > 0)
                {
                    lst.Add(MakeMessage("min", TextProvider.Message(MessageIds.invalid_min, lab, Min.ToString())));
                }

                if (Max > 0)
                {
                    lst.Add(MakeMessage("max", TextProvider.Message(MessageIds.invalid_max, lab, Max.ToString())));
                }



                return string.Join("", lst);
            }
        }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            List<ValidatorModel> lst = new List<ValidatorModel>();
            string lab = Label;

            if (Min > 0)
            {
                lst.Add(MakeModel("min", TextProvider.Message(MessageIds.invalid_min, lab, Min.ToString())));
            }

            if (Max > 0)
            {
                lst.Add(MakeModel("max", TextProvider.Message(MessageIds.invalid_max, lab, Max.ToString())));
            }

            return lst;
        }
    }
}
