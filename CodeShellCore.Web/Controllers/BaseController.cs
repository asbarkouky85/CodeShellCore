using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CodeShellCore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected InstanceStore<object> Store;
        protected SubmitResult SubmitResult { get; set; }

        public BaseController()
        {
            Store = new InstanceStore<object>(() => HttpContext.RequestServices);
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.LoadCultureFromHeader();
            base.OnActionExecuting(context);
        }

        protected T GetService<T>() where T : class
        {
            return Store.GetInstance<T>();
        }

        protected bool ModelIsValid()
        {
            SubmitResult = new SubmitResult(0);
            if (!ModelState.IsValid)
            {
                SubmitResult.Message = "Invalid Object";
                SubmitResult.Code = (int)HttpStatusCode.BadRequest;
                foreach (var item in ModelState.Keys)
                {
                    SubmitResult.Data[item] = ModelState[item];
                }
                return false;
            }
            return true;
        }

        protected bool ModelIsValid(out SubmitResult res)
        {
            res = new SubmitResult(0);
            if (!ModelState.IsValid)
            {
                res.Message = "Invalid Object";
                res.Code = (int)HttpStatusCode.BadRequest;
                foreach (var item in ModelState.Keys)
                {
                    res.Data[item] = ModelState[item];
                }
                return false;
            }
            return true;
        }
    }
}
