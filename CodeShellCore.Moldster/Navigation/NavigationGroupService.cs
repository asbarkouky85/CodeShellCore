using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Extensions.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Navigation
{
    public class NavigationGroupService : DtoEntityService<NavigationGroup, long, NavigationGroupDto, PagedListRequestDto>, INavigationGroupService
    {
        IMoldsterUnit _unit;

        public NavigationGroupService(IMoldsterUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public Task<PagedResult<NavigationGroupLookupDto>> GetAll(PagedListRequestDto opt)
        {
            var opts = opt.GetOptionsFor<NavigationGroupLookupDto>();
            return _unit.NavigationGroupRepository.FindAs(a => new NavigationGroupLookupDto { Id = a.Id, Name = a.Name }, opts);
        }

        public async Task<SubmitResult> CheckForUnorderedNavigationPages(long navigationGroupId)
        {
            if (await _unit.NavigationPageRepository.Exist(d => d.NavigationGroupId == navigationGroupId && d.DisplayOrder == 0))
            {
                var ps = await _unit.NavigationPageRepository.Find(d => d.NavigationGroupId == navigationGroupId);
                ps = ps.OrderBy(d => d.DisplayOrder).ToList();
                int i = 1;
                foreach (var p in ps)
                {
                    p.DisplayOrder = i++;
                    _unit.NavigationPageRepository.Update(p);
                }
            }
            return await _unit.SaveChanges();
        }

        public async Task<PagedResult<NavigationPageListDTO>> GetPagesByNav(long naveId, PagedListRequestDto opts)
        {
            await CheckForUnorderedNavigationPages(naveId);
            var op = opts.GetOptionsFor<NavigationPageListDTO>();
            op.AddFilter(d => d.NavigationGroupId == naveId);
            op.OrderProperty = "DisplayOrder";
            return await _unit.NavigationPageRepository.FindAndMap(op);
        }

        public Task<PagedResult<PageListDTO>> GetPageToAdd(PagedListRequestDto opt)
        {
            var opts = opt.GetOptionsFor<PageListDTO>();
            opts.AddFilter(a => a.HasRoute == true);
            opts.AddFilter(a => a.RouteParameters == null);
            return _unit.PageRepository.FindAndMap(opt.GetOptionsFor<PageListDTO>());
        }

        public async Task<SubmitResult> DeleteNavPage(long id)
        {
            _unit.NavigationPageRepository.DeleteById(id);
            return await _unit.SaveChanges();
        }

        public async Task<List<TenantDto>> GetTenant()
        {
            return await _unit.TenantRepository.FindAs(s => new TenantDto { Id = s.Id, Name = s.Name, Code = s.Code });
        }

        public async Task<SubmitResult> Create(List<NavigationPageDto> navigationPageListDTOs)
        {
            await _unit.NavigationPageRepository.ApplyChanges(navigationPageListDTOs, Mapper);
            return await _unit.SaveChanges();
        }

        public async Task<SubmitResult> CreateNave(NavigationGroupDto navigationGroup)
        {
            var item = await _unit.NavigationGroupRepository.FindAs(a => a.Name, x => x.Name == navigationGroup.Name);
            if (item.Count == 0)
            {
                var entity = Mapper.Map(navigationGroup, new NavigationGroup());
                _unit.NavigationGroupRepository.Add(entity);
                return await _unit.SaveChanges();
            }
            else
            {
                return new SubmitResult(code: 1, message: "this navigation added befor");
            }
        }

        public async Task<SubmitResult> SetApplyOrder(ApplyOrderDTO dto)
        {
            NavigationPage s = await _unit.NavigationPageRepository.FindSingle(a => a.Id == dto.SourceId);
            NavigationPage t = await _unit.NavigationPageRepository.FindSingle(a => a.Id == dto.TargetId);
            if (s != null && t != null)
            {
                int temp = s.DisplayOrder;
                s.DisplayOrder = t.DisplayOrder;
                t.DisplayOrder = temp;
                _unit.NavigationPageRepository.Update(s);
                _unit.NavigationPageRepository.Update(t);

                var page = (await _unit.PageRepository.FindAs(a => new PageDetailsDto
                {
                    TenantCode = a.Tenant.Code,
                    DomainName = a.Domain.NameChain
                }, x => x.Id == s.PageId)).First();
            }

            return await _unit.SaveChanges();
        }
    }
}
