using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    public interface ILookupLoaderController
    {
        IActionResult GetEditLookups([FromQuery]Dictionary<string, string> data);
        IActionResult GetListLookups([FromQuery]Dictionary<string, string> data);
    }
}
