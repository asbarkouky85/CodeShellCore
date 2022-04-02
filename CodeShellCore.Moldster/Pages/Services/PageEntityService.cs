using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Moldster.Pages
{
    public class PageEntityService : DtoEntityService<Page, long, LoadOptions, PageListDTO, CreatePageDTO, CreatePageDTO, PageDto>, IPageService
    {
        IDomainScriptGenerationService domainTs => Unit.ServiceProvider.GetService<IDomainScriptGenerationService>();
        IPageHtmlGenerationService _html => Unit.ServiceProvider.GetService<IPageHtmlGenerationService>();
        IPageScriptGenerationService pageTs => Unit.ServiceProvider.GetService<IPageScriptGenerationService>();
        public PageEntityService(IConfigUnit unit) : base(unit)
        {
        }

        public override SubmitResult<CreatePageDTO> Put(PageDto dto)
        {
            var res = base.Put(dto);
            if (res.IsSuccess && res.Data.TryGetValue("MoveRequest", out object req))
            {
                var r = (MovePageRequest)req;

                _html.MoveHtmlTemplate(r);
                pageTs.MoveScript(r);
                domainTs.GenerateDomainModule(r.TenantCode, r.FromPath.GetBeforeLast("/"));
                domainTs.GenerateDomainModule(r.TenantCode, r.ToPath.GetBeforeLast("/"));
                domainTs.GenerateRoutes(r.TenantCode);
            }
            return res;
        }

        public override DeleteResult Delete(long id)
        {
            var res = base.Delete(id);
            if (res.IsSuccess && res.Data.TryGetValue("ViewPath", out object data))
            {
                var path = (MovePageRequest)data;
                _html.DeleteHtmlTemplate(path.TenantCode, path.FromPath);
                pageTs.DeleteScript(path.TenantCode, path.FromPath);
                domainTs.GenerateDomainModule(path.TenantCode, path.FromPath.GetBeforeLast("/"));
                domainTs.GenerateRoutes(path.TenantCode);
            }
            return res;
        }
    }
}
