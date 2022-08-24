using CodeShellCore.Helpers;
using CodeShellCore.Moldster.PageCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Pages
{
    public class PageControlRepository : MoldsterRepository<PageControl, MoldsterContext>, IPageControlRepository
    {
        public PageControlRepository(MoldsterContext con) : base(con)
        {
        }

        public List<T> GetDtos<T>(Expression<Func<PageControl, bool>> filter = null)
        {
            var q = Loader;
            if (filter != null)
                q = q.Where(filter);

            return QueryDto<T>(q).ToList();
        }

        public void UpdateControls(long pageId, List<Control> controls, byte defaultAccess = 2)
        {
            IEnumerable<PageControl> existing = Loader.Where(d => d.PageId == pageId).ToList();
            foreach (Control c in controls)
            {
                if (!existing.Any(d => d.ControlId == c.Id))
                {
                    Add(new PageControl
                    {
                        Id = Utils.GenerateID(),
                        PageId = pageId,
                        ControlId = c.Id,
                        Accessability = defaultAccess
                    });
                }
            }

        }
    }
}
