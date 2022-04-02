using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder.Services;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Moldster.Sql.Dtos;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PagesController : BaseApiController, IPageService
    {
        PagesService _service;
        private readonly IPageService pageService;

        IPublisherService pub => GetService<IPublisherService>();
        EnvironmentAccessor acc => GetService<EnvironmentAccessor>();
        IPathsService paths => GetService<IPathsService>();

        public PagesController(PagesService service, IPageService pageService)
        {
            _service = service;
            this.pageService = pageService;
        }


        [HttpPut]
        public SubmitResult<CreatePageDTO> Put([FromBody] PageDto obj)
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
            return _service.SetViewParams(@params);
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

        public LoadResult<PageListDTO> Get([FromQuery] LoadOptions opt)
        {
            return pageService.Get(opt);
        }

        public IActionResult GetPagesByDomain([FromQuery] LoadOptions opts, [FromQuery] long domainId)
        {
            return Respond(_service.GetPagesByDomain(domainId, opts));
        }

        //public object GetEditLookups([FromQuery] Dictionary<string, string> data)
        //{
        //    return lookups.PageEdit(data);
        //}

        public IEnumerable<PageParameterDTO> GetViewParameters(long id)
        {
            return _service.GetViewParameters(id);
        }

        public PageCustomizationDTO GetCustomizationData(long id)
        {
            return _service.GetCustomizationData(id);
        }

        public SubmitResult ApplyCustomization([FromBody] PageCustomizationDTO dto)
        {
            return _service.ApplyCustomization(dto);
        }

        public LoadResult<PageListDTO> FindPages([FromQuery] LoadOptions opts, [FromBody] FindPageRequest request)
        {
            LoadResult<PageListDTO> pages = _service.FindPages(request, opts);
            return pages;
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

        public Dictionary<string, IEnumerable<Named<object>>> GetEditLookups(Dictionary<string, string> data)
        {
            return pageService.GetEditLookups(data);
        }

        public Dictionary<string, IEnumerable<Named<object>>> GetListLookups(Dictionary<string, string> data)
        {
            return pageService.GetListLookups(data);
        }

        public LoadResult<PageListDTO> GetCollection(string id, LoadOptions options)
        {
            return pageService.GetCollection(id, options);
        }
    }
}
