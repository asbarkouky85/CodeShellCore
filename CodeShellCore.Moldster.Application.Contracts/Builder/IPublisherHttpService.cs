using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Net;

namespace CodeShellCore.Moldster.Builder
{
    public interface IPublisherHttpService : IHttpService
    {
        PublisherResult HandleRequest(PublisherRequest req);
        Result UploadFile(string files, string folder);
        Result UploadFile(byte[] file, string folder);
        bool FileExists(string url);
    }
}
