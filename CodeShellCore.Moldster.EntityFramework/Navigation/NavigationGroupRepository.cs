using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Navigation
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

        public IEnumerable<T> GetTenantNavs<T>(long modId)
        {
            var s = from n in Loader
                    where n.NavigationPages.Any(d => d.Page.TenantId == modId)
                    select n;
            return QueryDto<T>(s).ToList();
        }
    }
}
