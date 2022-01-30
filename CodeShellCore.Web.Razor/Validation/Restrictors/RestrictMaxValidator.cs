using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Validation.Restrictors
{
    public class RestrictMaxValidator : Validator
    {
        protected int MAX;
        protected string Message;
        public override string Attribute { get { return "max='" + MAX + "' onkeydown='dontExceedThis(" + MAX + ",this,event)' onkeyup='dontExceedThis(" + MAX + ",this,event)'"; } }// 

        public RestrictMaxValidator(int max)
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
            var m = MakeModel("pattern", Message);
            return new ValidatorModel[] { m };
        }
    }
}

