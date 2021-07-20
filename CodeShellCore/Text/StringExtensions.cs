using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;



namespace CodeShellCore.Text
{
    public static class StringExtensions
    {
        static Inflector.Inflector Plurizer = new Inflector.Inflector(new CultureInfo("en"));
        static MD5 Md5Hash = MD5.Create();

        public static byte[] ToMD5Bytes(this string input)
        {
            return Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public static string Plurize(this string word)
        {
            return Plurizer.Pluralize(word);
        }

        public static string Singularize(this string word)
        {
            return Plurizer.Singularize(word);
        }

        public static string ToMD5(this string input)
        {

            byte[] data = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();

        }

        /// <summary>
        /// Serializes object to json string
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static string ToJson(this object ob, JsonSerializerSettings sett = null)
        {
            if (sett != null)
                return JsonConvert.SerializeObject(ob, sett);
            return JsonConvert.SerializeObject(ob);
        }

        /// <summary>
        /// Serializes object to json string
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static string ToJsonIndent(this object ob)
        {
            return JsonConvert.SerializeObject(ob, Formatting.Indented);
        }

        /// <summary>
        /// Serializes object to json string using formatting option
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="formatting"></param>
        /// <returns></returns>
        public static string ToJson(this object ob, Formatting formatting)
        {
            return JsonConvert.SerializeObject(ob, formatting);
        }

        public static string ToEnglishNumber(this string input)
        {
            string EnglishNumbers = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    EnglishNumbers += char.GetNumericValue(input, i);
                }
                else
                {
                    EnglishNumbers += input[i].ToString();
                }
            }
            return EnglishNumbers;
        }


        public static T FromJson<T>(this string st)
        {
            return JsonConvert.DeserializeObject<T>(st);
        }

        public static object FromJson(this string st, Type t)
        {
            return JsonConvert.DeserializeObject(st, t);
        }

        public static bool TryRead<T>(this string st, out T data) where T : class
        {
            try
            {
                data = JsonConvert.DeserializeObject<T>(st);
                return data != null;
            }
            catch
            {
                data = null;
                return false;
            }

        }
        /// <summary>
        /// Returns the string with lower case first letter
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static string LCFirst(this string st)
        {
            if (st.Length == 0)
                return st;
            return st.Substring(0, 1).ToLower() + st.Substring(1, st.Length - 1);
        }

        /// <summary>
        /// Returns the string with upper case first letter
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static string UCFirst(this string st)
        {
            return st.Substring(0, 1).ToUpper() + st.Substring(1, st.Length - 1);
        }

        /// <summary>
        /// Substracts the string after the last occurance of the given string
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="str">the given string</param>
        /// <returns></returns>
        public static string GetBeforeLast(this string subject, string str)
        {
            int ind = subject.LastIndexOf(str);
            if (ind > 0)
                return subject.Substring(0, ind);
            else
                return subject;
        }

        public static string GetBeforeFirst(this string subject, string str)
        {
            int ind = subject.IndexOf(str);
            if (ind > 0)
                return subject.Substring(0, ind);
            else
                return subject;
        }

        public static string GetAfterLast(this string subject, string str)
        {
            int ind = subject.LastIndexOf(str);
            if (ind != 0)
                return subject.Substring(ind + str.Length);
            else
                return subject;
        }

        public static string GetAfterFirst(this string subject, string str, int length = 0)
        {
            int ind = subject.IndexOf(str);
            if (ind > -1)
            {
                if (length == 0)
                    return subject.Substring(ind + str.Length);
                else
                    return subject.Substring(ind + str.Length, length);
            }

            else
                return subject;
        }

        public static int ConvertToInt(this String str)
        {
            int value = 0;
            int.TryParse(str, out value);
            return value;
        }

        public static double ConvertToDouble(this String str)
        {
            double value = 0;
            double.TryParse(str, out value);
            return value;
        }

        public static T ConvertTo<T>(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return default(T);
            var foo = TypeDescriptor.GetConverter(typeof(T));
            return (T)foo.ConvertFromInvariantString(str);
        }

        public static object ConvertTo(this string str, Type t)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            var foo = TypeDescriptor.GetConverter(t);
            return foo.ConvertFromInvariantString(str);
        }

        public static bool GetPatternContents(this string subject, string pattern, out string[] data)
        {
            Regex reg = new Regex(pattern);
            var s = reg.Match(subject);
            if (!s.Success)
            {
                data = new string[0];
                return false;
            }
            var lst = new List<string>();
            for (var i = 0; i < s.Groups.Count; i++)
            {
                if (i > 0)
                {
                    lst.Add(s.Groups[i].Value);
                }
            }
            data = lst.ToArray();
            return true;
        }

        public static long ConvertToLong(this String str)
        {
            long value = 0;
            long.TryParse(str, out value);
            return value;
        }
    }
}
