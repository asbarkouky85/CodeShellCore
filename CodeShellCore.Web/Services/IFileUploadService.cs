using System.Collections.Generic;
using CodeShellCore.Files;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Web.Services
{
    public interface IFileUploadService
    {
        List<TmpFileData> Upload(Dictionary<string, IFormFile> files);
    }
}