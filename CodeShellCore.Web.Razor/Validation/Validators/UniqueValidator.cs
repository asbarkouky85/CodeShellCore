using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Validation.Internal;

namespace CodeShellCore.Web.Razor.Validation.Validators.Angular
{
    public class UniqueValidator : Validator
    {
        string ServiceName;
        string Id;
        public UniqueValidator(string service,string idExpression)
        {

            ServiceName = service;
            Id = idExpression;
        }
        public override string Attribute { get { return $"is-unique [data-service]=\"{ServiceName}\" [item-id]=\"{Id}\" column-id=\"{ColumnId.GetAfterLast("__")}\""; } }

        public override string ValidationMessage { get { return MakeMessage("unique", TextProvider.Message(MessageIds.field_exists, TextProvider.Column(ColumnId))); } }
    }
}
