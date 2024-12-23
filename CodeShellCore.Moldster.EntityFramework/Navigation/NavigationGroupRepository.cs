using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Navigation
{

    public class NavigationGroupRepository : MoldsterRepository<NavigationGroup, MoldsterContext>, INavigationGroupRepository
    {

        public NavigationGroupRepository(
            MoldsterContext con) : base(con)
        {
        }

        public async Task<NavigationGroup> GetNavigationGroup(string name)
        {
            var gr = await Loader.FirstOrDefaultAsync(d => d.Name == name);
            if (gr == null)
            {
                gr = new NavigationGroup { Name = name };
                Add(gr);
            }
            return gr;
        }

        public async Task<IEnumerable<T>> GetTenantNavs<T>(long modId)
        {
            var s = from n in Loader
                    where n.NavigationPages.Any(d => d.Page.TenantId == modId)
                    select n;
            return await QueryDto<T>(s).ToListAsync();
        }
    }
}
