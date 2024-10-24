using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Views;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public virtual string GetMainComponent([FromQuery] PageAcquisitorDTO dto)
        {
            return service.GetMainComponent(dto.ViewPath);
        }

        public virtual async Task<RenderedPageResultDto> GetPageCategoryById(long id)
        {
            return await service.GetPageCategoryById(id);
        }

        public virtual async Task<List<PageConfigurationDto>> GetPagesByCategory(long id)
        {
            return await service.GetPagesByCategory(id);
        }
    }
}
