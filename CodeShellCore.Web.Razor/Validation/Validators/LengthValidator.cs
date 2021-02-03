using System.Collections.Generic;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;

namespace CodeShellCore.Web.Razor.Validation.Validators
{

    public class LengthValidator : Validator
    {
        protected int maxLength = 0;
        protected int minLength = 0;
        public override string Attribute
        {
            get
            {
                string data = "", sep = "";

                if (maxLength > 0)
                {
                    data += "maxlength=\"" + maxLength + "\"";
                    sep = " ";
                }


                if (minLength > 0)
                    data += sep + "minlength=\"" + minLength + "\"";
                return data;

            }
        }

        public LengthValidator(int _maxLength, int _minLength = 0)
        {
            maxLength = _maxLength;
            minLength = _minLength;
        }

        public override string ValidationMessage
        {
            get
            {
                string messages = "";
                if (minLength > 0 && maxLength > 0)
                {
                    messages = MakeMessage("minlength", TextProvider.Message(MessageIds.invalid_min_and_max_length, maxLength.ToString(), minLength.ToString()));
                    messages += "\n" + MakeMessage("maxlength", TextProvider.Message(MessageIds.invalid_min_and_max_length, maxLength.ToString(), minLength.ToString()));
                }
                else if (maxLength > 0)
                {
                    messages = MakeMessage("maxlength", TextProvider.Message(MessageIds.invalid_max_length, maxLength.ToString()));
                }
                else if (minLength > 0)
                {
                    messages = MakeMessage("minlength", TextProvider.Message(MessageIds.invalid_min_length, minLength.ToString()));
                }
                return messages;
            }
        }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            List<ValidatorModel> lst = new List<ValidatorModel>();

            if (minLength > 0 && maxLength > 0)
            {
                lst.Add(MakeModel("minlength", TextProvider.Message(MessageIds.invalid_min_and_max_length, maxLength.ToString(), minLength.ToString())));
                lst.Add(MakeModel("maxlength", TextProvider.Message(MessageIds.invalid_min_and_max_length, maxLength.ToString(), minLength.ToString())));
            }
            else if (maxLength > 0)
            {
                lst.Add(MakeModel("maxlength", TextProvider.Message(MessageIds.invalid_max_length, maxLength.ToString())));
            }
            else if (minLength > 0)
            {
                lst.Add(MakeModel("minlength", TextProvider.Message(MessageIds.invalid_min_length, minLength.ToString())));
            }
            return lst;
        }
    }
}
