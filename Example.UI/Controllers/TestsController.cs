using Asga.Auth;
using CodeShellCore.Linq;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
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

        public IActionResult TestSingle()
        {
            return Respond(new { Id = 1, Name = "Ahmed" });
        }

        public IActionResult TestMany()
        {
            var res = new LoadResult<User>();
            var l = new List<User>();
            l.Add(new Asga.Auth.User { Id = 1 });
            l.Add(new Asga.Auth.User { Id = 2 });
            l.Add(new Asga.Auth.User { Id = 3 });
            res.List = l;
            res.TotalCount = l.Count;

            return Respond(res);
        }
    }
}
