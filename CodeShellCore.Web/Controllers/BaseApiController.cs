using Microsoft.AspNetCore.Mvc;
using System.Net;

using CodeShellCore.Data.Helpers;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Web.Filters;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeShellCore.Web.Controllers
{
    [ApiExceptionFilter]
    [ApiAuthorize]
    public abstract class BaseApiController : BaseController
    {
        protected virtual IActionResult Respond<T>(LoadResult<T> ob) where T : class
        {
            if (ClientData.IsMobile)
            {
                return Json(new MobileLoadResult<T>(ob));
            }
            return Json(ob);
        }

        protected virtual IActionResult Respond(object ob, HttpStatusCode? code = null)
        {
            if (ClientData.IsMobile)
            {
                return Json(Result.MakeMobileResult(ob));
            }
            if (ob == null)
            {
                code = HttpStatusCode.NotFound;
                ob = new { };
            }

            Response.StatusCode = (int)(code ?? HttpStatusCode.OK);

            return Json(ob);
        }

        protected virtual IActionResult Respond(LoginResult ob)
        {
            if (ClientData.IsMobile)
            {
                var res = Result.MakeMobileResult(new
                {
                    userData = ob.UserData,
                    tokenData = new { token = ob.Token, tokenExpiry = ob.TokenExpiry }
                });
                res.Message = ob.Message;
                var ex = res.GetException();
                if (ex != null)
                    res.SetException(ex);
                res.Code = ob.Code;
                return Json(res);
            }

            Response.StatusCode = ob.IsSuccess ? 200 : 417;

            return Json(ob);
        }

        protected virtual IActionResult Respond(Result res = null)
        {
            if (res == null)
                res = SubmitResult ?? new SubmitResult();
            if (res.Code == 0)
                Response.StatusCode = (int)HttpStatusCode.OK;
            else
                Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;

            return new JsonResult(res);
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            if (context.Result is ObjectResult)
            {
                var obj = (ObjectResult)context.Result;
                if (obj.DeclaredType.Implements(typeof(IResult)) && !((IResult)obj.Value).IsSuccess)
                {
                    obj.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                }
            }
        }
    }
}
