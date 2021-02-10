using CodeShellCore.Text.Localization;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class MaxValueValidator : Validator
    {
        protected int MAX;
        protected string Message;
        public override string Attribute { get { return "max='" + MAX + "'"; } }// onkeydown='dontExceedThis("+ MAX + ",this,event)' onkeyup='dontExceedThis("+ MAX + ",this,event)'

        public MaxValueValidator(int max)
        {
            MAX = max;
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
