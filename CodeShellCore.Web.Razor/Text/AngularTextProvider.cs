﻿using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CodeShellCore.Web.Razor.Text
{
    public class AngularTextProvider : IRazorLocaleTextProvider
    {
        public CultureInfo Culture => Shell.DefaultCulture;

        public string Column(string index, string cult = null)
        {
            return "{{'Columns." + index + "' | translate }}";
        }

        string GetParamsString(params string[] formatElements)
        {
            string param = "";
            if (formatElements != null && formatElements.Length > 0)
            {
                var dic = new Dictionary<string, string>();
                int i = 0;
                foreach (string st in formatElements)
                {
                    dic["p" + (i++)] = _protect(st);
                }
                param += ": " + dic.ToJson().Replace("\"", "");
            }
            return param;
        }

        string _protect(string st)
        {
            if (st.Contains("{{"))
                return st.Replace("{{", "").Replace("}}", "");
            else if (!st.Contains("'"))
                return $"'{st}'";
            return st;
        }

        public string Message(string index, params string[] formatElements)
        {
            return "{{'Messages." + index + "' | translate " + GetParamsString(formatElements) + " }}";
        }

        public string Page(string index, string cult = null)
        {
            return "{{'Pages." + index + "' | translate }}";
        }

        public string Word(string index, string cult = null)
        {
            return "{{'Words." + index + "' | translate }}";
        }

        public string Word(string index, params string[] args)
        {
            return "{{'Words." + index + "' | translate " + GetParamsString(args) + " }}";
        }

        public string Word(Enum en, string cult = null)
        {
            return Word(en.GetString());
        }

        public string WordWithCulture(string index, string cult, params string[] args)
        {
            return "{{'Words." + index + "' | translate " + GetParamsString(args) + " }}";
        }

        public string MessageWithCulture(string index, string cult, params string[] formatElements)
        {
            return "{{'Messages." + index + "' | translate " + GetParamsString(formatElements) + " }}";
        }
    }
}
