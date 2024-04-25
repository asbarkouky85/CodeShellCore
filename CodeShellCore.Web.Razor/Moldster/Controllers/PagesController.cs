using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PagesController : BaseApiController, IPageEntityService
    {
        private readonly IPageEntityService pageService;

        IPublisherService pub => GetService<IPublisherService>();
        EnvironmentAccessor acc => GetService<EnvironmentAccessor>();
        IPathsService paths => GetService<IPathsService>();

        public PagesController(IPageEntityService pageService)
        {
            this.pageService = pageService;
        }

        [HttpPut]
        public SubmitResult<CreatePageDTO> Put([FromBody] CreatePageDTO obj)
        {
            return pageService.Put(obj);
        }

        [HttpPost]
        public SubmitResult<CreatePageDTO> Post([FromBody] CreatePageDTO obj)
        {
            return pageService.Post(obj);
        }

        public SubmitResult SetViewParams([FromBody] ViewParamsSetter @params)
        {
            return pageService.SetViewParams(@params);
        }

        public SubmitResult TenantCreated([FromBody] DbCreationRequest req)
        {

            acc.CurrentEnvironment = paths.GetEnvironments().Find(d => d.Name == req.Environment);

            if (acc.CurrentEnvironment != null)
            {
                pub.SetTenantInfo(req.TenantCode, null);
            }
            return SubmitResult;
        }

        public PagedResult<PageListDTO> Get([FromQuery] PagedListRequestDto opt)
        {
            return pageService.Get(opt);
        }

        public PagedResult<PageListDTO> GetPagesByDomain([FromQuery] long domainId, [FromQuery] PagedListRequestDto opts)
        {
            return pageService.GetPagesByDomain(domainId, opts);
        }

        public IEnumerable<PageParameterEditDto> GetViewParameters(long id)
        {
            return pageService.GetViewParameters(id);
        }

        public PageCustomizationDTO GetCustomizationData(long id)
        {
            return pageService.GetCustomizationData(id);
        }

        public SubmitResult ApplyCustomization([FromBody] PageCustomizationDTO dto)
        {
            return pageService.ApplyCustomization(dto);
        }

        public PagedResult<PageListDTO> FindPages([FromQuery] PagedListRequestDto opts, [FromBody] FindPageRequest request)
        {
            return pageService.FindPages(opts, request);
        }

        public DeleteResult Delete(long id)
        {
            return pageService.Delete(id);
        }

        public CreatePageDTO GetSingle(long id)
        {
            return pageService.GetSingle(id);
        }

        public bool IsUnique(IsUniqueDto dto)
        {
            return pageService.IsUnique(dto);
        }

        public Dictionary<string, IEnumerable<NamedDto<object>>> GetEditLookups(Dictionary<string, string> data)
        {
            return pageService.GetEditLookups(data);
        }

        public Dictionary<string, IEnumerable<NamedDto<object>>> GetListLookups(Dictionary<string, string> data)
        {
            return pageService.GetListLookups(data);
        }

        public PagedResult<PageListDTO> GetCollection(string id, PagedListRequestDto options)
        {
            return pageService.GetCollection(id, options);
        }

    }
}
