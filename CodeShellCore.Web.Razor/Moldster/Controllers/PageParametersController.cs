using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class PageParametersController : BaseApiController
    {
        private readonly IPageParameterDataService service;

        public PageParametersController(IPageParameterDataService service)
        {
            this.service = service;
        }

        public IActionResult GetReferences([FromBody] ParameterRequest req, [FromQuery] LoadOptions opt)
        {
            LoadResult<PageReferenceDTO> lst = service.GetReferences(req, opt);
            return Respond(lst);
        }
    }
}
