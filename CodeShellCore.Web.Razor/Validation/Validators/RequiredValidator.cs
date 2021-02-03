using System.Collections.Generic;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class RequiredValidator : Validator
    {
        public string RequiredCondition { get; protected set; }
        public RequiredValidator(string requiredIf = null)
        {
            RequiredCondition = requiredIf;
        }
        public override string ValidationMessage
        {
            get
            {
                return MakeMessage("required", TextProvider.Message(MessageIds.field_required, Label));
            }
        }

        public override string Attribute { get { return RequiredCondition == null ? "required" : "ng-required=\"" + RequiredCondition + "\""; } }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            return new[] { MakeModel("required", TextProvider.Message(MessageIds.field_required, Label)) };
        }
    }
}
