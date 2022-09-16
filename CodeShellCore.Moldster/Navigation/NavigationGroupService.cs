using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Navigation
{
    public class NavigationGroupService : EntityService<NavigationGroup>
    {
        IConfigUnit _unit;

        public NavigationGroupService(IConfigUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public LoadResult<NavigationGroupDTO> GetAll(LoadOptions opt)
        {
            var opts = opt.GetOptionsFor<NavigationGroupDTO>();
            return _unit.NavigationGroupRepository.FindAs(a => new NavigationGroupDTO { Id = a.Id, Name = a.Name }, opts);
        }

        public SubmitResult CheckForUnorderedNavigationPages(long navigationGroupId)
        {
            if (_unit.NavigationPageRepository.Exist(d => d.NavigationGroupId == navigationGroupId && d.DisplayOrder == 0))
            {
                var ps = _unit.NavigationPageRepository.Find(d => d.NavigationGroupId == navigationGroupId);
                ps = ps.OrderBy(d => d.DisplayOrder).ToList();
                int i = 1;
                foreach (var p in ps)
                {
                    p.DisplayOrder = i++;
                    _unit.NavigationPageRepository.Update(p);
                }
            }
            return _unit.SaveChanges();
        }

        public LoadResult<NavigationPageListDTO> GetPagesByNav(long naveId, LoadOptions opts)
        {
            CheckForUnorderedNavigationPages(naveId);
            var op = opts.GetOptionsFor<NavigationPageListDTO>();
            op.AddFilter(d => d.NavigationGroupId == naveId);
            op.OrderProperty = "DisplayOrder";
            return _unit.NavigationPageRepository.FindAndMap(op);
        }

        public LoadResult<PageListDTO> GetPageToAdd(LoadOptions opt)
        {
            var opts = opt.GetOptionsFor<PageListDTO>();
            opts.AddFilter(a => a.HasRoute == true);
            opts.AddFilter(a => a.RouteParameters == null);
            return _unit.PageRepository.FindAndMap(opt.GetOptionsFor<PageListDTO>());
        }

        public SubmitResult DeleteNavPage(long id)
        {
            _unit.NavigationPageRepository.DeleteById(id);
            return _unit.SaveChanges();
        }

        public List<Tenant> GetTenant()
        {
            return _unit.TenantRepository.FindAs(s => new Tenant { Id = s.Id, Name = s.Name, Code = s.Code }).ToList();
        }

        public SubmitResult Create(List<NavigationPage> navigationPageListDTOs)
        {
            _unit.NavigationPageRepository.ApplyChanges(navigationPageListDTOs);
            return _unit.SaveChanges();
        }

        public SubmitResult CreateNave(NavigationGroup navigationGroup)
        {
            var item = _unit.NavigationGroupRepository.FindAs(a => a.Name, x => x.Name == navigationGroup.Name);
            if (item.Count == 0)
            {
                _unit.NavigationGroupRepository.Add(navigationGroup);
                return _unit.SaveChanges();
            }
            else
            {
                return new SubmitResult(code: 1, message: "this navigation added befor");
            }
        }

        public SubmitResult SetApplyOrder(ApplyOrderDTO dto)
        {
            NavigationPage s = _unit.NavigationPageRepository.FindSingle(a => a.Id == dto.SourceId);
            NavigationPage t = _unit.NavigationPageRepository.FindSingle(a => a.Id == dto.TargetId);
            if (s != null && t != null)
            {
                int temp = s.DisplayOrder;
                s.DisplayOrder = t.DisplayOrder;
                t.DisplayOrder = temp;
                _unit.NavigationPageRepository.Update(s);
                _unit.NavigationPageRepository.Update(t);

                var page = _unit.PageRepository.FindAs(a => new PageDetailsDto
                {
                    TenantCode = a.Tenant.Code,
                    DomainName = a.Domain.NameChain
                }, x => x.Id == s.PageId).First();
            }

            return _unit.SaveChanges();
        }
    }
}
