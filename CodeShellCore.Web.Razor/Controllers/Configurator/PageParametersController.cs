using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public class PageParametersController : BaseApiController
    {
        private readonly IPageParameterDataService service;

        public PageParametersController(IPageParameterDataService service)
        {
            this.service = service;
        }

        public IActionResult GetReferences([FromBody] ParameterRequestDTO req, [FromQuery] LoadOptions opt)
        {
            LoadResult<PageReferenceDTO> lst = service.GetReferences(req, opt);
            return Respond(lst);
        }
    }
}
