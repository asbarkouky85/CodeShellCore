using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CodeShellCore.Linq;

namespace CodeShellCore.Moldster.Navigation
{
    public class NavigationPageRepository : MoldsterRepository<NavigationPage, MoldsterContext>, INavigationPageRepository
    {
        public NavigationPageRepository(MoldsterContext con) : base(con)
        {
        }

        public LoadResult<T> GetUnderNave<T>(long navId, LoadOptions opt) where T : class
        {
            var opts = opt.GetOptionsFor<T>();

            var q = from np in DbContext.NavigationPages
                    where np.NavigationGroupId == navId
                    orderby np.DisplayOrder
                    select np;
            return QueryDto<T>().LoadWith(opts);
        }

        public void SetDisplayOrder(long naveId)
        {
            var q = from np in DbContext.NavigationPages
                    where np.NavigationGroupId == naveId
                    orderby np.Page.Name
                    select np;
            var list = q.Select(a => new NavigationPage { Id = a.Id, NavigationGroupId = a.NavigationGroupId, PageId = a.PageId, DisplayOrder = a.DisplayOrder }).ToList();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].DisplayOrder = i + 1;
                    DbContext.Update(list[i]);
                }
                DbContext.SaveChanges();
            }

        }
    }
}
