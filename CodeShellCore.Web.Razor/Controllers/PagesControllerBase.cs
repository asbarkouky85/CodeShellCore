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

        [HttpPut]
        public IActionResult Put([FromBody] CreatePageDTO obj)
        {
            SubmitResult = Service.Update(obj);
            return Respond();
        }

        public IActionResult SetViewParams(long id, [FromBody]ViewParams @params)
        {
            SubmitResult = Service.SetViewParams(id, @params);
            return Respond();
        }
    }
}
