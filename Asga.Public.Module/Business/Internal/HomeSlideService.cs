using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Files.Uploads;
using CodeShellCore.Linq;

namespace Asga.Public.Business.Internal
{
    public class HomeSlideService : PublicEntityService<HomeSlide>, IHomeSlideService
    {
        protected readonly IPublicUnit unit;
        private readonly IUploadedFilesHandler uploaded;

        public HomeSlideService(IPublicUnit unit, IUploadedFilesHandler _uploaded) : base(unit)
        {
            this.unit = unit;
            uploaded = _uploaded;
        }

        public override LoadResult LoadObjects(LoadOptions opts)
        {
            var data = base.LoadObjects(opts);
            FixUrls((IEnumerable<HomeSlide>)data.List);
            return data;
        }

        protected virtual void FixUrls(IEnumerable<HomeSlide> list)
        {
            foreach (HomeSlide item in list)
            {
                item.Image = uploaded.GetUrlByPath(item.Image);
            }
        }

        public override LoadResult<HomeSlide> LoadCollection(string collectionId, LoadOptions opts)
        {
            var res = base.LoadCollection(collectionId, opts);
            FixUrls(res.ListT);
            return res;
        }

        public override HomeSlide GetSingle(object id)
        {
            var data = base.GetSingle(id);
            if (data != null)
                data.LoadFile(uploaded);
            return data;
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
