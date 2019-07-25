using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation.Validators
{
    public class AngularMinMaxValidator : MinMaxValidator
    {
        string minExp;
        string maxExp;
        public AngularMinMaxValidator(string max, string min = null) : base(0, 0)
        {
            minExp = min;
            maxExp = max;
        }

        public override string Attribute
        {
            get
            {
                if (minExp != null || maxExp != null)
                {
                    string str = "";
                    if (minExp != null)
                        str += "[min]='" + minExp + "'";

                    if (maxExp != null)
                        str += "[max]='" + maxExp + "'";

                    return str;
                }
                return base.Attribute;
            }
        }

        public override string ValidationMessage
        {
            get
            {
                if (minExp != null || maxExp != null)
                {
                    List<string> lst = new List<string>();
                    string lab = Label;

                    if (minExp!=null)
                    {
                        lst.Add(MakeMessage("min", TextProvider.Message(MessageIds.invalid_min, lab, minExp)));
                    }

                    if (maxExp!=null)
                    {
                        lst.Add(MakeMessage("max", TextProvider.Message(MessageIds.invalid_max, lab, maxExp)));
                    }
                    return string.Join("", lst);
                }
                return base.ValidationMessage;
            }
        }
    }
}
