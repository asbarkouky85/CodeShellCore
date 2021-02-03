using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class AngularPatternValidator : Validator
    {
        protected string Pattern;
        protected string Message;
        public override string Attribute { get { return "[pattern]=\"" + Pattern + "\""; } }

        public AngularPatternValidator(string pattern, string message = null)
        {
            Pattern = pattern;
            Message = message;
        }

        public override string ValidationMessage
        {
            get
            {
                if (Message == null)
                {
                    Message = TextProvider.Message(MessageIds.invalid_field, Label);
                }
                return MakeMessage("pattern", Message);
            }
        }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            if (Message == null)
            {
                Message = TextProvider.Message(MessageIds.invalid_field, Label);
            }
            return new[] { MakeModel("pattern", Message) };
        }
    }
}
