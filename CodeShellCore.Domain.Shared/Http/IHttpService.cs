using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodeShellCore.Files;

namespace CodeShellCore.Http
{
    public interface IHttpService
    {
        Dictionary<string, string> Headers { get; }
        string LogToFile { get; set; }

        FileBytes DownloadFile(string url, object query = null);
        Task<FileBytes> DownloadFileAsync(string url, object query = null);
        HttpResponseMessage Get(string url, object query = null);
        Task<HttpResponseMessage> GetAsync(string url, object query = null);
        Task<T> GetAsyncAs<T>(string url, object query = null) where T : class;
        HttpResponseMessage Post<T>(string url, T data, object query = null) where T : class;
        Task<HttpResponseMessage> PostAsync<T>(string url, T data, object query = null) where T : class;
        Task<HttpResponseMessage> UploadFiles(string url, IEnumerable<FileData> files, object query = null);
    }
}