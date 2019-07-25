using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    [HtmlExceptionFilter]
    [QueryAuthorizeFilter]
    public class BaseMvcController : BaseController
    {
    }
}
