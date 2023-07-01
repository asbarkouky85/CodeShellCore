
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Web.Razor.Models;

namespace CodeShellCore.Web.Razor.Validation.Restrictors
{
    public class RestrictAlphaNumeric : Validator
    {
        public override string Attribute { get { return "is-alpha-numeric"; } }

        public override string ValidationMessage { get { return ""; } }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            return new ValidatorModel[0];
        }
    }
}
