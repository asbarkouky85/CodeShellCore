using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Db.Razor;
using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Razor.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PagesControllerBase : EntityController<Page, long>
    {
        PagesService Service;
        public PagesControllerBase(PagesService service) : base(service)
        {
            Service = service;
        }


        [HttpPost]
        public IActionResult Post([FromBody] CreatePageDTO obj)
        {
            SubmitResult = Service.Create(obj);
            return Respond();
        }


        public IActionResult SetViewParams([FromBody]ViewParamsSetter @params)
        {
            SubmitResult = Service.SetViewParams(@params);
            return Respond();
        }
    }
}
