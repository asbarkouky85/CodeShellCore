using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    [ApiExceptionFilter]
    public class ViewsController : BaseController
    {
        private readonly IConfigUnit _unit;
        private readonly IViewsService service;

        public ViewsController(
            IViewsService service,
            IConfigUnit unit)
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

        public virtual IActionResult GetPage([FromQuery] PageAcquisitorDTO dto)
        {
            var html = service.GetPage(dto);
            return Content(html.TemplateContent);
        }

        public virtual IActionResult GetPageById(long id)
        {
            var html = service.GetPageById(id);
            return Content(html.TemplateContent);
        }

        public virtual IActionResult GetMainComponent([FromQuery] PageAcquisitorDTO dto)
        {
            var html = service.GetMainComponent(dto.ViewPath);
            return Content(html);
        }

    }
}
