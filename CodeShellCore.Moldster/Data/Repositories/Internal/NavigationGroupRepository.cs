using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Navigation.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
{

    public class NavigationGroupRepository : MoldsterRepository<NavigationGroup, MoldsterContext>, INavigationGroupRepository
    {

        public NavigationGroupRepository(
            MoldsterContext con) : base(con)
        {
        }

        public NavigationGroup GetNavigationGroup(string name)
        {
            var gr = Loader.FirstOrDefault(d => d.Name == name);
            if (gr == null)
            {
                gr = new NavigationGroup { Name = name };
                Add(gr);
            }
            return gr;
        }

        public IEnumerable<NavigationGroupDTO> GetTenantNavs(long modId)
        {
            var s = from n in Loader
                    where n.NavigationPages.Any(d => d.Page.TenantId == modId)
                    select new NavigationGroupDTO
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Pages = n.NavigationPages.Where(d => d.Page.TenantId == modId).OrderBy(d => d.DisplayOrder).Select(e => new NavigationPageDTO
                        {
                            ActionName = e.Page.SpecialPermission != null ? e.Page.SpecialPermission : (e.Page.ResourceActionId != null ? e.Page.ResourceAction.Name : null),
                            PrivilegeType = e.Page.PrivilegeType,
                            Apps = e.Page.Apps,
                            PageIdentifier = e.Page.Domain.Name + "__" + e.Page.Name,
                            ResourceName = e.Page.ResourceId == null ? null : e.Page.Resource.Name,
                            RouteParameters = e.Page.RouteParameters,
                            Url = e.Page.ViewPath
                        })
                    };
            return s.ToList();
        }
    }
}
