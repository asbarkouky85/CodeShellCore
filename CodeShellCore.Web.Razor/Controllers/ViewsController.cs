using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Views;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    [ApiExceptionFilter]
    public class ViewsController : BaseController
    {

        private readonly IViewsService service;

        public ViewsController(IViewsService service)
        {
            this.service = service;
        }

        public async Task<TemplateDataCollector> GetTemplateData(long id)
        {
            Response.ContentType = "application/json";
            return await service.GetTemplateData(id);
        }

        public virtual async Task<string> GetPage([FromQuery] PageAcquisitorDTO dto)
        {
            var html = await service.GetPage(dto);
            return html.TemplateContent;
        }

        public virtual async Task<string> GetPageById(long id)
        {
            var html = await service.GetPageById(id);
            return html.TemplateContent;
        }

        public virtual async Task<string> GetMainComponent([FromQuery] PageAcquisitorDTO dto)
        {
            return await service.GetMainComponent(dto.ViewPath);
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
