using Microsoft.AspNetCore.Mvc;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Web.Razor.Services;

namespace CodeShellCore.Web.Razor.Controllers
{
    public abstract class DbViewsControllerBase : BaseMoldsterViewsController
    {
        private readonly IConfigUnit _unit;
        private readonly ServerViewsService service;

        public DbViewsControllerBase(
            ServerViewsService service,
            IConfigUnit unit) : base(service)
        {
            _unit = unit;
            this.service = service;
        }

        public JsonResult GetTemplateData(long id)
        {
            return Json(service.GetTemplateData(id));
        }

        public ContentResult GetGuide(long id)
        {
            string code = _unit.TenantRepository.GetSingleValue(d => d.Code, d => d.Id == id);
            var view = service.GetGuide(code);

            return Content(view, "text/html");
        }
    }
}
