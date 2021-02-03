using CodeShellCore.Files;
using CodeShellCore.Net;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Moldster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CodeShellCore.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class MoldsterPublisherController : BaseApiController, IPublisherController
    {
        public MoldsterPublisherController()
        {

        }
        [HttpPost]
        [Route("HandleRequest")]
        public IActionResult HandleRequest([FromBody]PublisherRequest req)
        {
            var res = new PublisherResult
            {
                Code = 0,
                Message = "success"
            };
            try
            {
                string file = Path.Combine(Shell.AppRootPath, req.FileName);
                string folder = Path.Combine(Shell.AppRootPath, req.DestinationFolder);
                FileUtils.DecompressDirectory(file, folder);
                System.IO.File.Delete(file);
            }
            catch (Exception ex)
            {
                res.SetException(ex);
                res.Code = 500;
            }

            return Respond(res);
        }
    }

}
