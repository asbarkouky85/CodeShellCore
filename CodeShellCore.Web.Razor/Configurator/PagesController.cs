using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Configurator
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PagesController : EntityController<Page, long>
    {
        PagesService _service;
        IViewsService views => GetService<IViewsService>();
        IDbTemplateProcessingService cust => GetService<IDbTemplateProcessingService>();
        ConfiguratorLookupService lookups => GetService<ConfiguratorLookupService>();
        IAppFileHandler files => GetService<IAppFileHandler>();
        IScriptGenerationService scr => GetService<IScriptGenerationService>();
        IPublisherService pub => GetService<IPublisherService>();
        EnvironmentAccessor acc => GetService<EnvironmentAccessor>();
        IPathsService paths => GetService<IPathsService>();

        public PagesController(PagesService service) : base(service)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult Post([FromBody] CreatePageDTO obj)
        {
            SubmitResult = _service.Create(obj);
            return Respond();
        }

        public IActionResult SetViewParams([FromBody]ViewParamsSetter @params)
        {
            SubmitResult = _service.SetViewParams(@params);
            return Respond();
        }

        public IActionResult PageMoved([FromBody]MovePageRequest req)
        {
            files.MovePageFiles(req.TenantCode, req.FromPath, req.ToPath);

            scr.GenerateDomainModuleById(req.TenantCode, req.DomainId, true);
            scr.GenerateDomainModule(req.TenantCode, "Shared", true);
            return Respond();
        }

        public IActionResult TenantCreated([FromBody]DbCreationRequest req)
        {

            acc.CurrentEnvironment = paths.GetEnvironments().Find(d => d.Name == req.Environment);

            if (acc.CurrentEnvironment != null)
            {
                pub.SetTenantInfo(req.TenantCode, null);
            }
            return Respond();
        }

        public IActionResult Get([FromQuery] LoadOptions opt, [FromQuery] long tenantId)
        {
            return Respond(_service.GetAll(opt));
        }

        public IActionResult GetPagesByDomain([FromQuery]LoadOptions opts, [FromQuery] long domainId)
        {
            return Respond(_service.GetPagesByDomain(domainId, opts));
        }

        public IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(lookups.PageEdit(data));
        }

        public IActionResult GetViewParameters(long id)
        {
            IEnumerable<PageParameterDTO> lst = _service.GetViewParameters(id);
            return Respond(lst);
        }

        public IActionResult GetCustomizationData(long id)
        {
            PageCustomizationDTO data = _service.GetCustomizationData(id);
            return Respond(data);
        }

        public IActionResult ApplyCustomization([FromBody]PageCustomizationDTO dto)
        {
            SubmitResult = _service.ApplyCustomization(dto);

            return Respond();
        }

        public IActionResult FindPages([FromQuery]LoadOptions opts, [FromBody]FindPageRequest request)
        {
            LoadResult<PageListDTO> pages = _service.FindPages(request, opts);
            return Respond(pages);
        }

       

        public override IActionResult GetSingle([FromRoute] long id)
        {
            return Respond(_service.GetSinglePage(id));
        }

        public IActionResult Put([FromBody] CreatePageDTO dto)
        {
            SubmitResult = _service.UpdatePage(dto);
            return Respond();
        }

    }
}
