using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Net;
using CodeShellCore.Net.Ftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class PublisherHttpService : HttpService, IPublisherHttpService
    {
        private readonly EnvironmentAccessor accessor;
        string _baseUrl;
        public PublisherHttpService(EnvironmentAccessor accessor)
        {
            if (accessor.CurrentEnvironment.Upload == null)
                throw new Exception("no upload settings for current environment '" + accessor.CurrentEnvironment.Name + "'");
            _baseUrl = Utils.CombineUrl(accessor.CurrentEnvironment.Upload.ServerUrl, "api/Publisher");
            this.accessor = accessor;
        }
        protected override string BaseUrl => _baseUrl;

        public PublisherResult HandleRequest(PublisherRequest req)
        {
            return PostAs<PublisherResult>("HandleRequest", req);
        }

        public Result UploadFile(string file, string folder)
        {
            Result res = new Result();
            var conf = accessor.CurrentEnvironment.Upload;

            FTPClient client = new FTPClient(conf.Server, conf.UserName, conf.Password);
            client.Active = conf.Active;
            if (!File.Exists(file))
            {
                res.Code = 1;
                res.Message = "No such file ' " + file + " '";
                return res;
            }
            var byts = File.ReadAllBytes(file);
            return client.UploadFile(byts, folder);
        }

        public Result UploadFile(byte[] byts, string folder)
        {
            Result res = new Result();
            var conf = accessor.CurrentEnvironment.Upload;

            FTPClient client = new FTPClient(conf.Server, conf.UserName, conf.Password);
            client.Active = conf.Active;

            return client.UploadFile(byts, folder);
        }
    }
}
