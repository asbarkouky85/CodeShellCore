using CodeShellCore.Data.Lookups;
using CodeShellCore.Text;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CodeShellCore.Helpers
{
    public static class Utils
    {
        static Random r = new Random();
        static int Addition = 0;
        static int currentSecond;

        public static string CreatePropertyDictionary<T>(string folderPath, IEnumerable<string> ignore = null)
        {
            return CreatePropertyDictionary(typeof(T), folderPath);

        }

        public static T ReadDynamicAs<T>(dynamic attr) where T : class
        {
            string ser = JsonConvert.SerializeObject(attr);
            return ser.FromJson<T>();
        }

        public static string CreatePropertyDictionary(Type t, string folderPath, IEnumerable<string> ignore = null)
        {
            ignore = ignore ?? new string[0];
            List<string> st = new List<string>();
            IEnumerable<PropertyInfo> infos = t.GetProperties()
                .Where(d => (d.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(d.PropertyType)) &&
                    !ignore.Contains(d.Name));
            foreach (PropertyInfo inf in infos.OrderBy(d => d.Name))
            {
                st.Add(t.Name + "__" + inf.Name + "\t");
            }
            string filePath = Path.Combine(folderPath, t.Name + ".csv");
            File.WriteAllLines(filePath, st);
            return filePath;

        }

        public static List<Named<int>> GetNamedList<T>(string prefix = "")
        {
            var lst = new List<Named<int>>();
            foreach (Enum cond in Enum.GetValues(typeof(T)))
            {
                lst.Add(new Named<int>
                {
                    Id = Convert.ToInt32(cond),
                    Name = prefix + EnumExtensions.GetString(cond)
                });
            }
            return lst;
        }

        public static void CopyStringToClipBoard(string st)
        {
            File.WriteAllText("./clip", st);
            CopyFileContentsToClipBoard("./clip");
            File.Delete("./clip");
        }

        public static string RandomNumber(int digits)
        {
            string st = "";

            for (int i = 0; i < digits; i++)
            {
                st += r.Next(0, 9);
            }
            return st;
        }
        public static string RandomAlphabet(int numOfChars, int CapitalAndSmall = 0) //CapitalAndSmall( 0 = capital and small ,1=capital,2 =small)
        {
            string allalpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //26
            bool Case = CapitalAndSmall == 2 ? false : true;
            string st = "";

            for (int i = 0; i < numOfChars; i++)
            {

                if (Case && (CapitalAndSmall < 2))//Capital
                {
                    st += allalpha[r.Next(0, 25)].ToString().ToUpper();
                }
                else  //Small
                {
                    st += allalpha[r.Next(0, 25)].ToString().ToLower();
                }
                if (CapitalAndSmall == 0)
                    Case = !Case;
            }
            return st;

        }

        public static void RunCmdCommand(string command)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,

                    UseShellExecute = false
                }
            };
            process.Start();
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
        }

        public static void CopyFileContentsToClipBoard(string path)
        {
            RunCmdCommand("clip < " + path);

            Console.WriteLine();
            Console.WriteLine("Copied to clipboard");
            Console.WriteLine();
        }

        public static string CombineUrl(params string[] parts)
        {
            string ret = "";
            Regex reg = new Regex("^/");
            bool first = true;
            for (var i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                if (part == null || part.Length == 0)
                    continue;
                part = part.Replace("\\", "/");
                if (!first)
                    part = reg.Replace(part, "");
                
                first = false;
                if (i < (parts.Length - 1))
                {
                    char last = part[part.Length - 1];
                    part += (last != '/') ? "/" : "";
                }
                ret += part;
            }
            return ret;
        }

        public static string GetAsAbsolutePath(string folder)
        {
            if (folder[0] == '.')
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string removeDot = folder.Substring(1);
                folder = Path.Combine(path, removeDot);
            }
            return folder;
        }

        public static void CreateFolderForFile(string baseComponentPath)
        {
            FileInfo info = new FileInfo(baseComponentPath);
             if (!Directory.Exists(info.Directory.FullName))
                Directory.CreateDirectory(info.Directory.FullName);
        }

        public static string GetFileFolder(string file)
        {
            FileInfo info = new FileInfo(file);
            return info.Directory.FullName;
        }


        public static long GenerateID()
        {
            DateTime t = DateTime.Now;

            int thisSec = (int)t.TimeOfDay.TotalSeconds;

            if (thisSec == currentSecond)
                Addition++;
            else
                Addition = 0;

            currentSecond = thisSec;

            string st = (t.Year - 2000).ToString()
                + t.DayOfYear.ToString("D3")
                + currentSecond.ToString("D5")
                + Addition.ToString("D3");

            return long.Parse(st);
        }
    }
}
