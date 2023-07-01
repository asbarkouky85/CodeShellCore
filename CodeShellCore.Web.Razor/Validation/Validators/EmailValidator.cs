using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class EmailValidator : Validator
    {
        public override string Attribute { get { return @"ng-pattern=""/[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/"""; } }

        public override string ValidationMessage
        {
            get
            {
                return MakeMessage("pattern", TextProvider.Message(MessageIds.invalid_email));
            }
        }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            return new[] { MakeModel("pattern", TextProvider.Message(MessageIds.invalid_email)) };
        }
    }
}
