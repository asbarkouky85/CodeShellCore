using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Services.Http;
using CodeShellCore.Text;

namespace CodeShellCore.Files
{
    public class FileUtils
    {
        
        public static string GetThumbString(string path, int maxWidth, int maxHeight)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            FileInfo inf = new FileInfo(path);
            return inf.Name.GetBeforeLast(".") + "__" + maxWidth + "x" + maxHeight + inf.Extension;
        }

        public static string GetThumbPath(string path, ThumbSize size, string extension = null)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            FileInfo inf = new FileInfo(path);
            return size.ToString().ToLower() + "\\" + inf.Name.GetBeforeLast(".") + (extension ?? inf.Extension);
        }

        public static string GetThumbUrl(string path, ThumbSize size, string extension = null)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            FileInfo inf = new FileInfo(path);
            string name = inf.Name.GetBeforeLast(".") + (extension ?? inf.Extension);
            string url = inf.Directory.FullName.Replace(Shell.AppRootPath + "\\", "");
            url = url.Replace(Shell.PublicRoot + "\\", "");
            return Utils.CombineUrl(url, size.ToString().ToLower(), name);
        }


        public static string PathToUrl(string v)
        {
            FileInfo inf = new FileInfo(v);
            string url = inf.Directory.FullName.Replace(Shell.AppRootPath + "\\", "");
            url = url.Replace(Shell.PublicRoot + "\\", "");
            return Utils.CombineUrl(url, inf.Name).Replace("\\", "/");
        }

        public static FileBytes DownloadFile(string url,string logTo=null)
        {
            
            using (HttpService ser = HttpService.GetInstance())
            {
                if (logTo != null)
                    ser.LogToFile = logTo;

                return ser.DownloadFile(url);
            }
        }
    }
}
