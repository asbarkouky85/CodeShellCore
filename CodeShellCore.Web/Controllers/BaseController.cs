using CodeShellCore.Data.Helpers;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Helpers;
using CodeShellCore.Security;
using CodeShellCore.Services;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Net;

namespace CodeShellCore.Web.Controllers
{
    public class BaseController : Controller
    {
        protected InstanceStore<object> Store;
        protected SubmitResult SubmitResult { get; set; }
        
        public ClientData ClientData
        {
            get
            {
                return GetService<ClientData>();
            }
        }

        public IUserAccessor UserAccessor
        {
            get
            {
                return HttpContext.RequestServices.GetService<IUserAccessor>();
            }
        }

        public ILocaleTextProvider TextProvider
        {
            get
            {
                return GetService<ILocaleTextProvider>();
            }
        }

        public BaseController()
        {
            Store = new InstanceStore<object>(() => HttpContext.RequestServices);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.ProcessOnce();
            base.OnActionExecuting(context);
        }

        protected T GetService<T>() where T : class
        {
            return Store.GetInstance<T>();
        }

        protected virtual bool ModelIsValid()
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

        protected virtual bool ModelIsValid(out SubmitResult res)
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
