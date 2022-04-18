using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Text;
using System.Linq;

namespace CodeShellCore.Net.Ftp
{
    public class FTPClient
    {
        private string HomeFolder;
        private string UserName;
        private string Password;
        public bool Active { get; set; }

        public FTPClient(string server, string usr, string pswrd)
        {
            HomeFolder = "ftp://" + server;
            UserName = usr;
            Password = pswrd;
        }

        public bool Test()
        {
            FtpWebRequest request = CreateRequest(HomeFolder);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            var res = request.GetResponse() as FtpWebResponse;
            return res.StatusCode == FtpStatusCode.OpeningData;
        }

        public string[] GetDirectoryList()
        {
            DateTime t = DateTime.Now;

            FtpWebRequest request = CreateRequest(HomeFolder);
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            string line = "";
            List<string> filesList = new List<string>();

            while (stream.CanRead)
            {
                byte b = (byte)stream.ReadByte();

                if (b == 10)
                {

                    filesList.Add(line);
                    line = "";
                }
                else if (b != 13)
                {
                    line += (char)b;
                }
            }

            stream.Close();

            return filesList.ToArray();

        }

        public string[] GetDirectoryList(string directory = null)
        {
            string dir = HomeFolder;
            if (directory != null)
                dir = Utils.CombineUrl(HomeFolder, directory);

            FtpWebRequest request = CreateRequest(dir, WebRequestMethods.Ftp.ListDirectoryDetails);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            var arr = ReadAsArray(response);

            List<string> dirs = new List<string>();
            var s = new Regex("[ ]+");
            foreach (var d in arr)
            {
                var parts = s.Replace(d, ",").Split(',');
                if (parts.Length > 3)
                {
                    string type = parts[2];
                    if (type == "<DIR>")
                    {
                        dirs.Add(parts[3]);
                    }
                }
            }
            return dirs.ToArray();
        }

        public bool Exists(string url)
        {
            var directory = url.GetBeforeLast("/")+"/";
            var file = url.GetAfterLast("/");
            var files = GetFilesList(directory);
            return files.Contains(file);
        }

        string[] ReadAsArray(FtpWebResponse response)
        {
            Stream stream = response.GetResponseStream();
            string line = "";
            List<string> filesList = new List<string>();

            while (stream.CanRead)
            {
                byte b = (byte)stream.ReadByte();

                if (b == 10)
                {
                    filesList.Add(line);
                    line = "";
                }
                else if (b != 13)
                {
                    line += (char)b;
                }
            }

            stream.Close();
            return filesList.ToArray();
        }

        public string[] GetFilesList(string directory = null)
        {

            string dir = HomeFolder;
            if (directory != null)
                dir = Utils.CombineUrl(HomeFolder, directory);
            FtpWebRequest request = CreateRequest(dir);

            byte[] arr = new byte[256];

            //request.Timeout = 5000;
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return ReadAsArray(response);

        }

        public void DeleteDirectory(string fil)
        {
            string url = Utils.CombineUrl(HomeFolder, fil);
            FtpWebRequest request = CreateRequest(url);
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;

            WebResponse resp = request.GetResponse();
            Stream res = resp.GetResponseStream();
            res.Close();
        }

        public void DeleteFile(string fil)
        {
            string url = Utils.CombineUrl(HomeFolder, fil);
            FtpWebRequest request = CreateRequest(url);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            WebResponse resp = request.GetResponse();
            Stream res = resp.GetResponseStream();
            res.Close();
        }

        public string GetExtension(string fileSt)
        {
            if (fileSt.Length < 4)
                return "";

            return fileSt.Substring((fileSt.Length - 4), 4);
        }

        public Result UploadFile(byte[] file, string path)
        {
            var url = Utils.CombineUrl(HomeFolder, path);
            FtpWebRequest req = CreateRequest(url);
            req.ReadWriteTimeout = 5000;
            req.Method = WebRequestMethods.Ftp.UploadFile;

            req.ContentLength = file.Length;
            Stream str = req.GetRequestStream();
            str.Write(file, 0, file.Length);
            str.Close();
            GetResponse(req);
            req.Abort();

            return new Result();
        }

        public FtpResult DownloadFile(string fileLocation)
        {
            FtpWebRequest req = CreateRequest(Utils.CombineUrl(HomeFolder, fileLocation));
            req.Method = WebRequestMethods.Ftp.DownloadFile;
            return GetResponse(req);

        }

        public FtpResult GetResponse(FtpWebRequest req)
        {
            var res = new FtpResult();
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();

                Stream stream = response.GetResponseStream();
                byte[] bytes = new byte[0];

                using (MemoryStream receiver = new MemoryStream())
                {
                    byte[] arr = new byte[256];
                    int size;
                    int total = 0;

                    while ((size = stream.Read(arr, 0, arr.Length)) != 0)
                    {
                        total += size;
                        receiver.Write(arr, 0, size);
                    }

                    bytes = receiver.GetBuffer();
                }
                stream.Close();

                res = new FtpResult
                {
                    Code = (int)response.StatusCode,
                    Bytes = bytes,
                    StatusCode = response.StatusCode
                };
                return res;
            }
            catch (Exception ex)
            {
                res = new FtpResult();
                res.SetException(ex);
                res.Code = 1;
                return res;
            }
        }

        public FtpWebRequest CreateRequest(string requestString, string method = null)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestString);
            request.Credentials = new NetworkCredential(UserName, Password);

            request.UsePassive = !Active;
            request.UseBinary = true;
            request.KeepAlive = true;
            if (method != null)
            {
                request.Method = method;
                if(method== WebRequestMethods.Ftp.ListDirectory || method==WebRequestMethods.Ftp.ListDirectoryDetails)
                {
                    request.KeepAlive = false;
                }
            }
            return request;
        }
    }
}


