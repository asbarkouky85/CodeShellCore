using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Text;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.PageCategories.Services;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Moldster.Domains.Services;
using CodeShellCore.Moldster.Builder.Services;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Sql.Dtos;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PagesController : EntityController<Page, long>
    {
        PagesService _service;
        IViewsService views => GetService<IViewsService>();
        IPageCategoryHtmlService process => GetService<IPageCategoryHtmlService>();
        IPageHtmlGenerationService _html => GetService<IPageHtmlGenerationService>();
        ConfiguratorLookupService lookups => GetService<ConfiguratorLookupService>();
        IPageScriptGenerationService pageTs => GetService<IPageScriptGenerationService>();
        IDomainScriptGenerationService domainTs => GetService<IDomainScriptGenerationService>();
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

        public IActionResult SetViewParams([FromBody] ViewParamsSetter @params)
        {
            SubmitResult = _service.SetViewParams(@params);
            return Respond();
        }

        public IActionResult TenantCreated([FromBody] DbCreationRequest req)
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

        public IActionResult GetPagesByDomain([FromQuery] LoadOptions opts, [FromQuery] long domainId)
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

        public IActionResult ApplyCustomization([FromBody] PageCustomizationDTO dto)
        {
            SubmitResult = _service.ApplyCustomization(dto);

            return Respond();
        }

        public IActionResult FindPages([FromQuery] LoadOptions opts, [FromBody] FindPageRequest request)
        {
            LoadResult<PageListDTO> pages = _service.FindPages(request, opts);
            return Respond(pages);
        }



        public override IActionResult GetSingle([FromRoute] long id)
        {
            var ps = _service.GetSinglePage(id);
            return Respond(ps);
        }

        public IActionResult Put([FromBody] CreatePageDTO dto)
        {
            SubmitResult = _service.UpdatePage(dto);
            if (SubmitResult.IsSuccess && SubmitResult.Data.TryGetValue("MoveRequest", out object req))
            {
                var r = (MovePageRequest)req;

                _html.MoveHtmlTemplate(r);
                pageTs.MoveScript(r);
                domainTs.GenerateDomainModule(r.TenantCode, r.FromPath.GetBeforeLast("/"));
                domainTs.GenerateDomainModule(r.TenantCode, r.ToPath.GetBeforeLast("/"));
                domainTs.GenerateRoutes(r.TenantCode);
            }
            return Respond();
        }

        public override IActionResult Delete(long id)
        {
            SubmitResult = _service.DeleteById(id);
            if (SubmitResult.IsSuccess && SubmitResult.Data.TryGetValue("ViewPath", out object data))
            {
                var path = (MovePageRequest)data;
                _html.DeleteHtmlTemplate(path.TenantCode, path.FromPath);
                pageTs.DeleteScript(path.TenantCode, path.FromPath);
                domainTs.GenerateDomainModule(path.TenantCode, path.FromPath.GetBeforeLast("/"));
                domainTs.GenerateRoutes(path.TenantCode);
            }
            return Respond();
        }

    }
}
