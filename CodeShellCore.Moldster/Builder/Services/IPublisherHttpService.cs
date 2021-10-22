using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Builder.Services
{
    public interface IPublisherHttpService : IHttpService
    {
        PublisherResult HandleRequest(PublisherRequest req);
        Result UploadFile(string files, string folder);
        Result UploadFile(byte[] file, string folder);
    }
}
