using CodeShellCore.Text.Localization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Validation.Restrictors
{
    public class RestrictEnglishValidator : Validator
    {
  
        protected string Message;
        public override string Attribute { get { return "is-english"; } }

        public RestrictEnglishValidator()
        {
           
        }

        public override string ValidationMessage
        {
            get
            {
                return "";
            }
        }
    }
}
