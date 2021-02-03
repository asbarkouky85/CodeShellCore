using System.Collections.Generic;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class NumericTextValidator : Validator
    {
        public override string Attribute { get { return "ng-pattern='/[+-]?([0-9]*[.])?[0-9]+/'"; } }

        public override string ValidationMessage { get { return MakeMessage("pattern", TextProvider.Message(MessageIds.must_be_numeric, Label)); } }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            var m = MakeModel("pattern", TextProvider.Message(MessageIds.must_be_numeric, Label));
            return new[] { m };
        }
    }
}
