using CodeShellCore.Linq;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public class PageParametersController : BaseApiController
    {
        private readonly IPageParameterDataService service;

        public PageParametersController(IPageParameterDataService service)
        {
            this.service = service;
        }

        public async Task<PagedResult<PageReferenceDTO>> GetReferences([FromBody] ParameterRequest req, [FromQuery] PagedListRequestDto opt)
        {
            return await service.GetReferences(req, opt);
        }
    }
}
