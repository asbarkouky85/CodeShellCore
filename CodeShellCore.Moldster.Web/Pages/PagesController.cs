using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
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
        public Task<EntitySubmitResult<CreatePageDTO>> Put([FromBody] CreatePageDTO obj)
        {
            return pageService.Put(obj);
        }

        [HttpPost]
        public Task<EntitySubmitResult<CreatePageDTO>> Post([FromBody] CreatePageDTO obj)
        {
            return pageService.Post(obj);
        }

        public Task<SubmitResult> SetViewParams([FromBody] ViewParamsSetter @params)
        {
            return pageService.SetViewParams(@params);
        }

        public async Task<SubmitResult> TenantCreated([FromBody] DbCreationRequest req)
        {

            acc.CurrentEnvironment = paths.GetEnvironments().Find(d => d.Name == req.Environment);

            if (acc.CurrentEnvironment != null)
            {
                await pub.SetTenantInfo(req.TenantCode, null);
            }
            return SubmitResult;
        }

        public Task<PagedResult<PageListDTO>> Get([FromQuery] PagedListRequestDto opt)
        {
            return pageService.Get(opt);
        }

        public Task<PagedResult<PageListDTO>> GetPagesByDomain([FromQuery] long domainId, [FromQuery] PagedListRequestDto opts)
        {
            return pageService.GetPagesByDomain(domainId, opts);
        }

        public Task<IEnumerable<PageParameterEditDto>> GetViewParameters(long id)
        {
            return pageService.GetViewParameters(id);
        }

        public Task<PageCustomizationDTO> GetCustomizationData(long id)
        {
            return pageService.GetCustomizationData(id);
        }

        public Task<SubmitResult> ApplyCustomization([FromBody] PageCustomizationDTO dto)
        {
            return pageService.ApplyCustomization(dto);
        }

        public Task<PagedResult<PageListDTO>> FindPages([FromQuery] PagedListRequestDto opts, [FromBody] FindPageRequest request)
        {
            return pageService.FindPages(opts, request);
        }

        public Task<DeleteResult> Delete(long id)
        {
            return pageService.Delete(id);
        }

        public Task<CreatePageDTO> GetSingle(long id)
        {
            return pageService.GetSingle(id);
        }

        public Task<bool> IsUnique(IsUniqueDto dto)
        {
            return pageService.IsUnique(dto);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data)
        {
            return pageService.GetEditLookups(data);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> data)
        {
            return pageService.GetListLookups(data);
        }

        public Task<PagedResult<PageListDTO>> GetCollection(string id, PagedListRequestDto options)
        {
            return pageService.GetCollection(id, options);
        }

    }
}
