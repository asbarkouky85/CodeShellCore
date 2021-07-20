using CodeShellCore.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Json
{
    public static class JsonExtensions
    {
        public static JObject CreatePathJObject(this JObject ob, string path, object init = null)
        {
            init = init ?? new { };
            var splt = path.Split(':');
            var current = ob;
            for (var i = 0; i < splt.Length; i++)
            {
                var name = splt[i];

                var jObj = (JObject)current.GetValue(name);
                if (jObj == null)
                {
                    jObj = JObject.FromObject(init);
                    current.Add(name, jObj);

                }
                current = jObj;

            }
            return current;
        }

        public static JObject GetPathJObject(this JObject ob, string path)
        {
            var splt = path.Split(':');
            var current = ob;
            for (var i = 0; i < splt.Length; i++)
            {
                var name = splt[i];
                if (i == splt.Length - 1)
                {
                    var o = current.GetValue(name);
                    return (JObject)o;
                }
                else
                {
                    current = (JObject)current.GetValue(name);
                    if (current == null)
                    {
                        break;
                    }
                }
            }
            return current;
        }

        public static T GetPathValueAs<T>(this JObject ob, string path)
        {
            var pathObject = ob.GetPathAsString(path);
            return pathObject.ConvertTo<T>();
        }

        public static string GetPathAsString(this JObject ob, string path)
        {
            var splt = path.Split(':');
            var current = ob;
            for (var i = 0; i < splt.Length; i++)
            {
                var name = splt[i];
                if (i == splt.Length - 1)
                {
                    var o = current.GetValue(name);
                    return o?.ToString();
                }
                else
                {
                    current = (JObject)current.GetValue(name);
                    if (current == null)
                    {
                        break;
                    }
                }
            }
            return current.ToString();
        }

        public static T GetPathAs<T>(this JObject ob, string path) where T : class
        {
            var pathObject = ob.GetPathJObject(path);
            return pathObject?.ToObject<T>();
        }

        public static void SetPathValue<T>(this JObject ob, string path, T val) where T : class
        {
            if (!path.Contains(":"))
            {
                ob[path] = JToken.FromObject(val);
                return;
            }
                
            var pathObject = ob.CreatePathJObject(path.GetBeforeLast(":"));
            pathObject[path.GetAfterLast(":")] = JToken.FromObject(val);
        }
    }
}
