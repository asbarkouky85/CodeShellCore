
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class CustomValidator : Validator
    {
        private string Id;
        private string Message;
        private string Attr;
        public CustomValidator(string validatorId, string message, string attr = null)
        {
            Id = validatorId;
            Message = message;
            Attr = attr ?? "";
        }

        public override string Attribute { get { return Attr; } }

        public override string ValidationMessage { get { return MakeMessage(Id, Message); } }
    }
}
