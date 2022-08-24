using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Net;
using CodeShellCore.Net.Ftp;
using System;
using System.IO;

namespace CodeShellCore.Moldster.Builder
{
    public class PublisherHttpService : HttpService, IPublisherHttpService
    {
        UploadConfig Config
        {
            get
            {
                var up = accessor.CurrentEnvironment?.Upload;
                if (up == null)
                    return new UploadConfig { Type = "DEV" };
                return up;
            }
        }
        string _baseUrl;
        private readonly EnvironmentAccessor accessor;

        public PublisherHttpService(EnvironmentAccessor accessor)
        {
            this.accessor = accessor;
            _baseUrl = Utils.CombineUrl(Config.ServerUrl, "api/Publisher");

        }
        protected override string BaseUrl => _baseUrl;

        public PublisherResult HandleRequest(PublisherRequest req)
        {
            return PostAs<PublisherResult>("HandleRequest", req);
        }

        public Result UploadFile(string file, string folder)
        {
            Result res = new Result();
            var conf = Config;

            try
            {
                FTPClient client = new FTPClient(conf.Server, conf.UserName, conf.Password);
                client.Active = conf.Active;
                if (!File.Exists(file))
                {
                    res.Code = 1;
                    res.Message = "No such file ' " + file + " '";
                    return res;
                }
                var byts = File.ReadAllBytes(file);
                res = client.UploadFile(byts, folder);
            }
            catch (Exception e)
            {
                res.SetException(e);
            }
            return res;
        }

        public Result UploadFile(byte[] byts, string folder)
        {
            Result res = new Result();
            try
            {
                var conf = Config;

                FTPClient client = new FTPClient(conf.Server, conf.UserName, conf.Password);
                client.Active = conf.Active;

                res = client.UploadFile(byts, folder);
            }
            catch (Exception e)
            {
                res.SetException(e);

            }
            return res;

        }

        public bool FileExists(string url)
        {
            Result res = new Result();
            try
            {
                var conf = Config;

                FTPClient client = new FTPClient(conf.Server, conf.UserName, conf.Password);
                client.Active = conf.Active;

                return client.Exists(url);
            }
            catch (Exception e)
            {
                res.SetException(e);
                throw e;
            }
        }
    }
}
