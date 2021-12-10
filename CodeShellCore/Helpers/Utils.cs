using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
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
using System.Threading;

namespace CodeShellCore.Helpers
{
    public enum CharType { Capital, Small, Both }
    public static class Utils
    {
        static Random r = new Random();
        static int Addition = 0;
        static int currentSecond;

        public static string CreatePropertyDictionary<T>(string folderPath, IEnumerable<string> ignore = null)
        {
            return CreatePropertyDictionary(typeof(T), folderPath);

        }

        public static int CompareVersions(string v1, string v2)
        {
            if (string.IsNullOrEmpty(v1) || string.IsNullOrEmpty(v2))
                throw new ArgumentOutOfRangeException("v1");

            string[] v1Parts = v1.Split('.');
            string[] v2Parts = v2.Split('.');

            int max = (new[] { v1Parts.Length, v2Parts.Length }).Max();
            for (var i = 0; i < max; i++)
            {
                int v1N = v1Parts.Length > i ? int.Parse(v1Parts[i]) : 0;
                int v2N = v2Parts.Length > i ? int.Parse(v2Parts[i]) : 0;
                if (v1N > v2N)
                    return 1;
                else if (v1N < v2N)
                    return 2;
            }
            return 0;
        }

        public static T ReadDynamicAs<T>(dynamic attr) where T : class
        {
            string ser = JsonConvert.SerializeObject(attr);
            return ser.FromJson<T>();
        }

        public static string CreateClientToken(AppClient cl, DateTime? expire = null, string provider = null)
        {
            expire = expire ?? DateTime.MaxValue;
            var jw = new ClientJwt
            {
                ClientId = cl.ClientId,
                Secret = cl.Secret,
                StartTime = DateTime.Now,
                ExpireTime = expire.Value,
                Provider = provider
            };
            return Shell.Encryptor.Encrypt(jw.ToJson());
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

        public static string GetSolutionFolder(Assembly assembly = null)
        {
            assembly = assembly ?? Shell.ProjectAssembly;
            return AppDomain.CurrentDomain.BaseDirectory.GetBeforeFirst("\\" + assembly.GetName().Name);
        }

        public static string GetPathFromConfig(string key, bool required = true)
        {
            var sol = GetSolutionFolder();
            string st = Shell.GetConfigAs<string>(key, required);
            return st?.Replace("{PARENT}", sol);
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

        public static string RandomAlphaNumeric(int numOfChars, CharType? type = null)
        {
            type = type ?? CharType.Small;
            string allalpha = "012345689ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var len = allalpha.Length;
            string st = "";

            for (int i = 0; i < numOfChars; i++)
            {
                switch (type)
                {
                    case CharType.Capital:
                        st += allalpha[r.Next(0, len)].ToString();
                        break;
                    case CharType.Small:
                        st += allalpha[r.Next(0, len)].ToString().ToLower();
                        break;
                    case CharType.Both:
                        var n = r.Next(0, 2);
                        var l = n == 0 ? false : true;
                        var c = allalpha[r.Next(0, len)].ToString();
                        if (!l)
                            c = c.ToLower();
                        st += c;
                        break;
                }
            }
            return st;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOfChars"></param>
        /// <param name="type">Default small</param>
        /// <returns></returns>
        public static string RandomAlphabet(int numOfChars, CharType? type = null)
        {
            type = type ?? CharType.Small;
            string allalpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var len = allalpha.Length;
            string st = "";

            for (int i = 0; i < numOfChars; i++)
            {
                switch (type)
                {
                    case CharType.Capital:
                        st += allalpha[r.Next(0, len)].ToString();
                        break;
                    case CharType.Small:
                        st += allalpha[r.Next(0, len)].ToString().ToLower();
                        break;
                    case CharType.Both:
                        var n = r.Next(0, 2);
                        var l = n == 0 ? false : true;
                        var c = allalpha[r.Next(0, len)].ToString();
                        if (!l)
                            c = c.ToLower();
                        st += c;
                        break;
                }
            }
            return st;
        }

        public static void RunCmdCommand(string command, string workingDirectory = null)
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory
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
                if (string.IsNullOrEmpty(part))
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

        public static void DeleteEmptyDirectories(string folder)
        {
            var dir = Directory.GetDirectories(folder, "*", SearchOption.AllDirectories);
            foreach (var d in dir)
            {
                if (Directory.Exists(d) && !Directory.GetFiles(d, "*", SearchOption.AllDirectories).Any())
                {
                    DeleteDirectory(d);
                }
            }
        }

        public static bool DeleteDirectory(string path)
        {
            try
            {
                ClearDirectory(path);
                Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool ClearDirectory(string path)
        {
            try
            {
                var fls = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (var f in fls)
                    File.Delete(f);
                var dirs = Directory.GetDirectories(path);
                foreach (var f in dirs)
                    Directory.Delete(f, true);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static string GetFileFolder(string file)
        {
            FileInfo info = new FileInfo(file);
            return info.Directory.FullName;
        }

        public static int GenerateIntID()
        {
            DateTime t = DateTime.Now;

            int thisSec = (int)t.TimeOfDay.TotalSeconds;

            if (thisSec == currentSecond)
            {
                Addition++;
                if (Addition >= 8)
                    Thread.Sleep(100);
            }
            else
            {
                Addition = 0;
            }


            currentSecond = thisSec;

            string st = (t.Year - 2000).ToString()
                + t.DayOfYear.ToString("D3")
                + currentSecond.ToString("D5")
                + Addition.ToString();

            return int.Parse(st);
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
            Console.WriteLine(st);
            return long.Parse(st);
        }


    }
}
