using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Controllers
{
    [QueryAuthorizeFilter(AllowAnonymous = true)]
    public abstract class SpaHomeController : BaseMvcController
    {
        public async virtual Task Index()
        {
            await GetService<ISpaFallbackHandler>().HandleRequestAsync(HttpContext);
        }
    }
}
