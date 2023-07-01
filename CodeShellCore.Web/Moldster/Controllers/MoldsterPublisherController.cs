using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Net;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Moldster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CodeShellCore.Web.Moldster.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public abstract class MoldsterPublisherController : BaseApiController, IPublisherController
    {
        public MoldsterPublisherController()
        {

        }
        [HttpPost]
        [Route("HandleRequest")]
        public virtual PublisherResult HandleRequest([FromBody] PublisherRequest req)
        {
            var res = new PublisherResult
            {
                Code = 0,
                Message = "success"
            };
            try
            {
                switch (req.Type)
                {
                    case ServerRequestTypes.Decompress:
                        Decompress(req);
                        break;
                    case ServerRequestTypes.DeleteDirectory:
                        DeleteDirectory(req);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                res.SetException(ex);
                res.Code = 500;
            }

            return res;
        }

        void DeleteDirectory(PublisherRequest req)
        {
            if (Directory.Exists(req.DestinationFolder))
                Utils.DeleteDirectory(req.DestinationFolder);
        }

        private void Decompress(PublisherRequest req)
        {
            string file = Path.Combine(Shell.AppRootPath, req.FileName);
            string folder = Path.Combine(Shell.AppRootPath, req.DestinationFolder);

            FileUtils.DecompressDirectory(file, folder);
            if (req.DeleteFileAfter == true)
                System.IO.File.Delete(file);
        }
    }

}
