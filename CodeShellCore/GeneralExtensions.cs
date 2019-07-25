using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeShellCore
{
    public static class GeneralExtensions
    {
        public static string GetMessageRecursive(this Exception ex, bool ignorInvocationException = true)
        {

            if (ex is TargetInvocationException && ignorInvocationException)
            {
                if (ex.InnerException != null)
                    return ex.InnerException.GetMessageRecursive();
                return "";
            }
            else
            {
                string message = " (" + ex.GetType().Name + ")  " + ex.Message;
                if (ex.InnerException != null)
                    message += " >> " + ex.InnerException.GetMessageRecursive(ignorInvocationException);
                return message;
            }


        }

        public static int AsInt(this Enum val)
        {
            return (int)Convert.ChangeType(val, typeof(int));
        }

        public static long AsLong(this Enum val)
        {
            return (long)Convert.ChangeType(val, typeof(long));
        }

        public static string GetEnumString<T>(this long id)
        {
            var s = Convert.ChangeType(id, typeof(T));
            return (s as Enum).GetString();
        }

        public static string[] GetStackTrace(this Exception ex, bool recurse = false, bool ignorInvocationException = true)
        {

            if (ex is TargetInvocationException && ignorInvocationException)
            {
                if (ex.InnerException != null)
                    return ex.InnerException.GetStackTrace();
                return new string[] { };
            }
            else if (recurse)
            {
                List<string> message = ex.StackTrace.Split('\r', '\n').Where(d => d.Length > 0).ToList();
                if (ex.InnerException != null)
                {
                    var inner = ex.InnerException.GetStackTrace(recurse, ignorInvocationException);
                    message.Add("");
                    message.Add("INNER STACK");
                    message.Add("");
                    message.AddRange(inner);
                }

                return message.ToArray();
            }
            else
            {
                return ex.StackTrace.Split('\r', '\n').Where(d => d.Length > 0).ToArray();
            }


        }

        public static object GetPropertyValue(this object obj, string prop)
        {
            var inf = obj.GetType().GetProperty(prop);
            if (inf != null)
                return inf.GetValue(obj);
            return null;
        }

        public static bool HasProperty(this object obj, string property)
        {
            return obj.GetType().GetProperties().Any(d => d.Name == property);
        }
    }
}
