using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using CodeShellCore.Text;
using CodeShellCore.Text.ResourceReader;

namespace CodeShellCore.Text.Localization
{
    public class LangUtils
    {
        public static string ResourceToJson(string assembly, string resourceFile, string cult)
        {
            var ass = Assembly.Load(assembly);
            ResourceManager man = new ResourceManager(assembly + "." + resourceFile, ass);
            Dictionary<string, string> dictionary = ResourceToDictionary(man, new CultureInfo(cult));

            return DictionaryToJson(dictionary);
        }

        private static string DictionaryToJson(Dictionary<string, string> dictionary)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var o in dictionary.OrderBy(d => d.Key))
            {

                if (string.IsNullOrEmpty(o.Value))
                    dic.Add(o.Key, IdToPhrase(o.Key));
                else
                    dic.Add(o.Key, o.Value);

            }
            return dic.ToJson(Newtonsoft.Json.Formatting.Indented);
        }

        public static ResourceContainer ReadFile(string path)
        {
            ResxXmlReader reader = new ResxXmlReader();
            if (reader.TryRead(path, out ResourceContainer cont))
            {
                return cont;
            }
            return null;
        }

        public static string ResourceToJson(string resourceFile)
        {
            ResxXmlReader reader = new ResxXmlReader();
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (reader.TryRead(resourceFile, out ResourceContainer cont))
            {
                if (cont.DataItems != null)
                {
                    foreach (var s in cont.DataItems)
                    {
                        dic[s.Name] = s.Value;
                    }
                }
                
            }
            return DictionaryToJson(dic);
        }

        public static Dictionary<string, string> ResourceToDictionary(Assembly ass, string fullResourceName, CultureInfo info)
        {
            ResourceManager man = new ResourceManager(fullResourceName, ass);
            return ResourceToDictionary(man, info);
        }

        public static Dictionary<string, string> ResourceToDictionary(ResourceManager man, CultureInfo info)
        {
            var dic = new Dictionary<string, string>();
            try
            {
                ResourceSet st = man.GetResourceSet(info, true, true);

                foreach (DictionaryEntry v in st)
                {
                    dic[v.Key.ToString()] = v.Value.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dic;
        }

        public static string CamelCaseToWords(string id, string separator)
        {
            Regex r = new Regex("[A-Z]");
            MatchCollection col = r.Matches(id);
            int i = 0;

            foreach (Match d in col)
            {
                if (d.Index != 0)
                {
                    id = id.Insert(d.Index + (i++), separator);
                }
            }
            return id;
        }

        public static string IdToPhrase(string id)
        {
            if (id.Contains("__"))
                id = id.GetAfterLast("__");

            id = id.Replace("_", " ");
            Regex r = new Regex("[A-Z]");
            MatchCollection col = r.Matches(id);
            int i = 0;

            foreach (Match d in col)
            {
                if (d.Index != 0)
                {
                    id = id.Insert(d.Index + (i++), " ");
                }
            }

            string[] words = id.Split(' ');
            string[] joiners = new string[] { "At", "To", "Of", "The", "And", "From", "In" };
            string res = "";

            for (int ii = 0; ii < words.Length; ii++)
            {
                if (ii != 0)
                    words[ii] = words[ii].ToLower();
                string sep = (ii == (words.Length - 1) || words[ii].Length == 1) ? "" : " ";
                res += words[ii] + sep;
            }

            return res;
        }
    }
}
