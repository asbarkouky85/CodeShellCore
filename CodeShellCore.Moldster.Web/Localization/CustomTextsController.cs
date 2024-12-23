using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Localization
{
    public class CustomTextsController : BaseApiController, ICustomTextService
    {
        private readonly ICustomTextService service;

        public CustomTextsController(ICustomTextService serv)
        {
            service = serv;
        }

        public Task<PagedResult<CustomTextDto>> Get(CustomTextRequestDto req, PagedListRequestDto opts)
        {
            return service.Get(req, opts);
        }

        public Task<SubmitResult> SaveChanges(IEnumerable<CustomTextDto> lst)
        {
            return service.SaveChanges(lst);
        }
    }
}
