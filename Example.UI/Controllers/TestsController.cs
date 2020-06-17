using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Controllers
{
    [ApiAuthorize(AllowAnonymous =true)]
    public class TestsController : BaseApiController
    {
        public object TestLocalization()
        {
            var prov = GetService<ILocaleTextProvider>();
            return new
            {
                Word = prov.Word("Login"),
                Message = prov.Message("delete_confirm_message"),
                pMessage = prov.Message("test_message", "Word1", "كلمه"),
                Column = prov.Page("Login")
            };
        }
    }
}
