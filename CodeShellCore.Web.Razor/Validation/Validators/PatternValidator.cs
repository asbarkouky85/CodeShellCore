using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Validation.Internal;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class PatternValidator : Validator
    {
        protected string Pattern;
        protected string Message;
        public override string Attribute { get { return "ng-pattern='/" + Pattern + "/'"; } }

        public PatternValidator(string pattern, string message = null)
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
    }
}
