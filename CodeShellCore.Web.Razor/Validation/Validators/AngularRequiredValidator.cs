using CodeShellCore.Text.Localization;

using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class AngularRequiredValidator : RequiredValidator
    {
        
        public override string Attribute { get { return RequiredCondition == null ? "required" : "[required]=\"" + RequiredCondition + "\""; } }

        public override string ValidationMessage { get { return MakeMessage("required", TextProvider.Message(MessageIds.field_required,TextProvider.Column(ColumnId))); } }

        public AngularRequiredValidator(string requiredIf = null)
        {
            RequiredCondition = requiredIf;
        }
    }
}
