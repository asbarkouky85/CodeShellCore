using System.Collections.Generic;
using CodeShellCore.Files;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Web.Services
{
    public interface IFileUploadService
    {
        List<TempFileDto> Upload(Dictionary<string, IFormFile> files);
    }
}