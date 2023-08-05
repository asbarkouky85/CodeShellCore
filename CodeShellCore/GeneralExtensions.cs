using CodeShellCore.Http;
using CodeShellCore.Text;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore
{
    public static class GeneralExtensions
    {
        public static bool FromBit(this int num, int bitOrder)
        {
            int data = (int)Math.Pow(2D, bitOrder);
            bool val = ((num & data) >> bitOrder) == 1;
            return val;
        }

        public static int SetBit(this int num, int bitOrder, bool value)
        {
            int data = (int)Math.Pow(2D, bitOrder);

            if (value == true)
                num = num | data;
            else if (num >= data)
                num = num - data;
            return num;
        }

        public static string GetMessageRecursive(this Exception ex, bool ignorInvocationException = true)
        {

            if (((ex is TargetInvocationException) || (ex is AggregateException)) && ignorInvocationException)
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

        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            
            var ins = services.FirstOrDefault(d => d.ServiceType == typeof(T));
            return (T)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }

        [NotNull]
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            var conf = services.GetConfigurationOrNull();
            if (conf == null)
                throw new Exception("Could not find an implementation of " + typeof(IConfiguration).AssemblyQualifiedName + " in the service collection.");
            return conf;
        }

        [CanBeNull]
        public static IConfiguration GetConfigurationOrNull(this IServiceCollection services)
        {
            var hostBuilderContext = services.GetSingletonInstanceOrNull<HostBuilderContext>();
            if (hostBuilderContext?.Configuration != null)
            {
                return hostBuilderContext.Configuration as IConfigurationRoot;
            }

            return services.GetSingletonInstanceOrNull<IConfiguration>();
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

            if (((ex is TargetInvocationException) || (ex is AggregateException)) && ignorInvocationException)
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

                    message.Add("-------");
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

        public static string ToHexCode(this Color color)
        {
            var argb = color.ToArgb();
            var d = (argb & 0xFFFFFF).ToString("X6");
            return "#" + d;
        }

        public static async Task<T> ReadAsAsync<T>(this HttpResponseMessage data) where T : class
        {
            if (data.IsSuccessStatusCode)
            {
                string responseString = await data.Content.ReadAsStringAsync();
                var res = responseString.FromJson<T>();
                return res;
            }
            else
            {
                throw new CodeShellHttpException(data);
            }
        }
    }
}
