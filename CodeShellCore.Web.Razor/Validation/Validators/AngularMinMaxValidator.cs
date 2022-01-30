using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class AngularMinMaxValidator : MinMaxValidator
    {
        string minExp;
        string maxExp;
        string _message;
        public AngularMinMaxValidator(string max, string min = null, string message = null) : base(0, 0)
        {
            minExp = min;
            maxExp = max;
            _message = message;
        }

        public override string Attribute
        {
            get
            {
                if (minExp != null || maxExp != null)
                {
                    string str = "numberRange ";
                    if (minExp != null)
                        str += "min='" + minExp + "'";

                    if (maxExp != null)
                        str += " [max]='" + maxExp + "'";

                    return str;
                }
                return base.Attribute;
            }
        }

        public override string ValidationMessage
        {
            get
            {
                var vMessage = "";
                string lab = Label;
                if (!string.IsNullOrEmpty(_message))
                {
                    vMessage = MakeMessage("number_range", TextProvider.Message(_message, lab, minExp, maxExp));
                }
                else if (!string.IsNullOrEmpty(minExp) && !string.IsNullOrEmpty(maxExp))
                {
                    vMessage = MakeMessage("number_range", TextProvider.Message(MessageIds.invalid_min_and_max, lab, minExp, maxExp));
                }
                else if (!string.IsNullOrEmpty(minExp))
                {
                    vMessage = MakeMessage("number_range", TextProvider.Message(MessageIds.invalid_min, lab, minExp));
                }
                else if (!string.IsNullOrEmpty(maxExp))
                {
                    vMessage = MakeMessage("number_range", TextProvider.Message(MessageIds.invalid_max, lab, maxExp));
                }
                return vMessage;
            }
        }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            List<ValidatorModel> lst = new List<ValidatorModel>();
            string lab = Label;
            var vMessage = "";
            if (!string.IsNullOrEmpty(_message))
            {
                vMessage = TextProvider.Message(_message, lab, minExp??"", maxExp??"");
            }
            else if (!string.IsNullOrEmpty(minExp) && !string.IsNullOrEmpty(maxExp))
            {
                vMessage = TextProvider.Message(MessageIds.invalid_min_and_max, lab, minExp, maxExp);
            }
            else if (!string.IsNullOrEmpty(minExp))
            {
                vMessage = TextProvider.Message(MessageIds.invalid_min, lab, minExp);
            }
            else if (!string.IsNullOrEmpty(maxExp))
            {
                vMessage = TextProvider.Message(MessageIds.invalid_max, lab, maxExp);
            }
            lst.Add(new ValidatorModel
            {
                Index = "number_range",
                Text = vMessage
            });
            return lst;
        }
    }
}
