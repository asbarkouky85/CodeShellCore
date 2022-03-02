using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Data.Helpers;

namespace Asga.Public.Business.Internal
{
    public class HomeSlideService : PublicEntityService<HomeSlide>, IHomeSlideService
    {
        protected readonly IPublicUnit unit;

        public HomeSlideService(IPublicUnit unit) : base(unit)
        {
            this.unit = unit;
        }

        public virtual SubmitResult SetActive(long id, bool state)
        {
            var m = Repository.FindSingle(id);
            m.IsActive = state;
            Repository.Update(m);
            return unit.SaveChanges();
        }

        public virtual SubmitResult SetSorting(IEnumerable<long> ids)
        {
            var all = Repository.Find(d => true);
            int i = 1;
            foreach (var s in ids)
            {
                var item = all.FirstOrDefault(d => d.Id == s);
                if (item != null)
                {
                    item.DisplayOrder = i++;
                    Repository.Update(item);
                }
            }
            return unit.SaveChanges();
        }
    }
}
