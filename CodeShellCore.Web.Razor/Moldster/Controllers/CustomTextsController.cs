using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Localization.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class CustomTextsController : BaseApiController,ICustomTextService
    {
        private readonly ICustomTextService service;

        public CustomTextsController(ICustomTextService serv)
        {
            service = serv;
        }

        public LoadResult<CustomTextDto> Get([FromBody] CustomTextRequest req, [FromQuery] LoadOptions opts)
        {
            return service.Get(req, opts);
        }

        public SubmitResult SaveChanges(IEnumerable<CustomTextDto> lst)
        {
            return service.SaveChanges(lst);
        }
    }
}
