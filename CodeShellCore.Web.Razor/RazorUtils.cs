using CodeShellCore.Data;
using CodeShellCore.Text;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Web.Razor
{
    public static class RazorUtils
    {
        internal static string ProcessProperty(string propName, object value)
        {
            string name = "";
            if (propName.Contains("calc__"))
            {
                string x = propName.GetAfterLast("__");
                name = "[" + x.Replace('_', '-') + "]";
            }
            else if (propName.Contains("evnt__"))
            {
                string x = propName.GetAfterLast("__");
                name = "(" + x.Replace('_', '-') + ")";
            }
            else if (propName.Contains("star__"))
            {
                string x = propName.GetAfterLast("__");
                name = "*" + x.Replace('_', '-');
            }
            else
            {
                name = propName.ToLower().Replace('_', '-');
            }


            return name + "=\"" + value + "\"";
        }

        public static string ToAttributeStringDynamic(dynamic attr)
        {
            if (attr == null)
                return "";
            string ser = JsonConvert.SerializeObject(attr);
            Dictionary<string, object> ss = ser.FromJson<Dictionary<string, object>>();
            List<string> lst = new List<string>();

            foreach (var pair in ss)
            {
                lst.Add(ProcessProperty(pair.Key, pair.Value));
            }
            string st = " "+string.Join(" ", lst);

            return st;
        }

        public static string ToAttributeString(object attr)
        {
            if (attr == null)
                return "";
            List<string> lst = new List<string>();

            PropertyInfo[] props = attr.GetType().GetProperties();
            foreach (var pair in props)
            {
                lst.Add(ProcessProperty(pair.Name, pair.GetValue(attr)));
            }
            string st = " "+string.Join(" ", lst);

            return st;
        }

        public static string GetIdentifier<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            string member = GetMemberName(expression);
            return (typeof(T).RealModelType().Name + "__" + member.Replace(".", "__")).ToLower();
        }

        public static string GetColumnId<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            string col = RazorConfig.ExpressionStringifier.GetColumnId((MemberExpression)expression.Body);
            bool isSub = col.Contains(".");
            col = col.Replace(".", "__");
            return (isSub) ? col : typeof(T).RealModelType().Name + "__" + col;
        }

        public static MemberExpression GetMemberExpression<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            MemberExpression exp = (MemberExpression)expression.Body;
            return (exp.Expression is MemberExpression) ? (MemberExpression)exp.Expression : exp;
        }

        public static string GetButtonClass(BtnClass cls)
        {
            return RazorConfig.Theme.GetButtonClass(cls);
        }

        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            return RazorConfig.ExpressionStringifier.GetMemberName((MemberExpression)expression.Body);
        }

        

        public static string UrlToPageId(string url)
        {
            Regex reg = new Regex(@"^\/");
            if (reg.IsMatch(url))
                url = url.Substring(1);

            return url.Replace("/", "__");
        }
    }
}
