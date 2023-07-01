using CodeShellCore.Linq.Stringifiers;
using CodeShellCore.Moldster;
using CodeShellCore.Text;
using CodeShellCore.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
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
            else if (propName.Contains("twb__"))
            {
                string x = propName.GetAfterLast("__");
                name = "[(" + x.Replace('_', '-') + ")]";
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
            string st = " " + string.Join(" ", lst);

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
            string st = " " + string.Join(" ", lst);

            return st;
        }

        public static string GetIdentifier<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            string member = GetMemberName(expression);
            return (typeof(T).GetEntityName() + "__" + member.Replace(".", "__")).ToLower();
        }

        public static string GetColumnId<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            string col = RazorConfig.ExpressionStringifier.GetColumnId((MemberExpression)expression.Body);
            bool isSub = col.Contains(".");
            col = col.Replace(".", "__");
            return (isSub) ? col : typeof(T).GetEntityName() + "__" + col;
        }

        public static MemberExpression GetMemberExpression<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            MemberExpression exp = (MemberExpression)expression.Body;
            return (exp.Expression is MemberExpression) ? (MemberExpression)exp.Expression : exp;
        }

        public static string ApplyConvension(string value, AppParts part)
        {
            return RazorConfig.NameService.ApplyConvension(value, part);
        }

        public static string GetButtonClass(BtnClass cls)
        {
            return RazorConfig.Theme.GetButtonClass(cls);
        }

        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            return RazorConfig.ExpressionStringifier.GetMemberName((MemberExpression)expression.Body);
        }

        public static string GetMemberNameDefault<T, TValue>(Expression<Func<T, TValue>> expression)
        {
            return new DefaultExpressionStringifier().GetMemberName((MemberExpression)expression.Body);
        }

        public static string UrlToPageId(string url)
        {
            Regex reg = new Regex(@"^\/");
            if (reg.IsMatch(url))
                url = url.Substring(1);
            string[] spl = url.Split('/');
            var len = spl.Length;

            if (spl.Length >= 2)
                return RazorConfig.NameService.ReverseConvention(spl[len - 2]) + "__" + RazorConfig.NameService.ReverseConvention(spl[len - 1]);
            else
                return url;

        }
    }
}
