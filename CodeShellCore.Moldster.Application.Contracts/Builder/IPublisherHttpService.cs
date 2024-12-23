using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Net;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public interface IPublisherHttpService : IHttpService
    {
        Task<PublisherResult> HandleRequest(PublisherRequest req);
        Task<Result> UploadFile(string files, string folder);
        Task<Result> UploadFile(byte[] file, string folder);
        Task<bool> FileExists(string url);
    }
}
