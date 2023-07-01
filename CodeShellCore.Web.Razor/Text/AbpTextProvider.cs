using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Text;

namespace CodeShellCore.Web.Razor.Text
{
    public class AbpTextProvider : IRazorLocaleTextProvider
    {
        public CultureInfo Culture => Shell.DefaultCulture;

        private string _default(string index)
        {
            if(!index.Contains("::"))
            {
                index = "::"+index;
            }
            return "{{'" + index + "' | abpLocalization }}";
        }

        private string _default(string index, params string[] parms)
        {
            if (!index.Contains("::"))
            {
                index = "::" + index;
            }
            return "{{'" + index + "' | abpLocalization :" + GetParamsString(parms) + "}}";
        }

        public string Column(string index, string cult = null)
        {
            return _default(index);
        }

        string GetParamsString(params string[] formatElements)
        {
            for (var i = 0; i < formatElements.Length; i++)
            {
                formatElements[i] = _protect(formatElements[i]);
            }
            return string.Join(":", formatElements);
        }

        string _protect(string st)
        {
            if (st.Contains("{{"))
                return "(" + st.Replace("{{", "").Replace("}}", "") + ")";
            else if (!st.Contains("'"))
                return $"'{st}'";
            return st;
        }

        public string Message(string index, params string[] formatElements)
        {
            return _default(index, formatElements);
        }

        public string Page(string index, string cult = null)
        {
            return _default(index);
        }

        public string Word(string index, string cult = null)
        {
            return _default(index);
        }

        public string Word(string index, params string[] args)
        {
            return _default(index, args);
        }

        public string Word(Enum en, string cult = null)
        {
            return Word(en.GetString());
        }

        public string WordWithCulture(string index, string cult, params string[] args)
        {
            return _default(index, args);
        }

        public string MessageWithCulture(string index, string cult, params string[] formatElements)
        {
            return _default(index, formatElements);
        }
    }
}
