using Microsoft.AspNetCore.Mvc;
using System.Net;

using CodeShellCore.Data.Helpers;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Web.Filters;

namespace CodeShellCore.Web.Controllers
{
    [ApiExceptionFilter]
    [ApiAuthorize]
    public class BaseApiController : BaseController
    {
        public IActionResult Respond(LoginResult res)
        {
            if (!res.Success)
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            
            return Json(res);

        }

        public IActionResult Respond(object ob, HttpStatusCode? code = null)
        {

            if (ob == null)
            {
                code = HttpStatusCode.NotFound;
                ob = new { };
            }

            Response.StatusCode = (int)(code ?? HttpStatusCode.OK);

            return Json(ob);
        }

        public IActionResult Respond(SubmitResult res=null)
        {
            if (res == null)
                res = SubmitResult;
            if (res.Code == 0)
                Response.StatusCode = (int)HttpStatusCode.OK;
            else
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;

            return new JsonResult(res);
        }

    }
}
