using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Net;

namespace CodeShellCore.Moldster.Builder.Services
{
    public interface IPublisherHttpService : IHttpService
    {
        PublisherResult HandleRequest(PublisherRequest req);
        Result UploadFile(string files, string folder);
        Result UploadFile(byte[] file, string folder);
    }
}
