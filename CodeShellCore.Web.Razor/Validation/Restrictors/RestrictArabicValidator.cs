﻿using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Razor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Validation.Restrictors
{
    public class RestrictArabicValidator : Validator
    {

        protected string Message;
        public override string Attribute { get { return "is-arabic"; } }

        public RestrictArabicValidator()
        {

        }

        public override string ValidationMessage
        {
            get
            {
                return "";
            }
        }

        public override IEnumerable<ValidatorModel> GetMessageModels()
        {
            return new ValidatorModel[0];
        }
    }
}
