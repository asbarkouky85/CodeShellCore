using CodeShellCore.Text.Localization;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class NumericTextValidator : Validator
    {
        public override string Attribute { get { return "ng-pattern='/[+-]?([0-9]*[.])?[0-9]+/'"; } }

        public override string ValidationMessage { get { return MakeMessage("pattern", TextProvider.Message(MessageIds.must_be_numeric, Label)); } }
    }
}
