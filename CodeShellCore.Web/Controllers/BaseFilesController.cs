using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Controllers
{
    public abstract class BaseFilesController : BaseApiController
    {
        protected IFileUploadService Service { get { return GetService<IFileUploadService>(); } }

        [HttpPost]
        public virtual IActionResult Upload()
        {
            Dictionary<string, IFormFile> dictionary = new Dictionary<string, IFormFile>();
            foreach (var f in Request.Form.Files)
            {
                dictionary[f.Name] = f;
            }
            return Respond(Service.Upload(dictionary));
        }
    }
}
