using System.Collections.Generic;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;

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

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            if (Message == null)
            {
                Message = TextProvider.Message(MessageIds.invalid_field, Label);
            }
            var n = MakeModel("pattern", Message);
            return new[] { n };
        }
    }
}
