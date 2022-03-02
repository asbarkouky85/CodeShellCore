using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Files;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asga.Public.Data.Internal
{
    public class HomeSlideRepository : AsgaPublicRepository<HomeSlide, AsgaPublicContext>, IHomeSlideRepository
    {
        int lcid;
        public HomeSlideRepository(AsgaPublicContext con, ICollectionConfigService service, IUserAccessor acc, Language lang) : base(con, service, acc)
        {
            lcid = lang.Culture.LCID;
        }

        protected virtual IQueryable<HomeSlide> QueryLocalized(IQueryable<HomeSlide> q = null)
        {
            q = q ?? Loader;
            return q.Select(d => new HomeSlide
            {
                Text = DbContext.GetLocalized("HomeSlide", d.Id, lcid, "Text", d.Text),
                Id = d.Id,
                CreatedBy = d.CreatedBy,
                CreatedOn = d.CreatedOn,
                Title = DbContext.GetLocalized("HomeSlide", d.Id, lcid, "Title", d.Title),
                IsMobile = d.IsMobile,
                Image = d.Image,
                IsActive = d.IsActive,
                Url = d.Url,
                DisplayOrder = d.DisplayOrder,
                UpdatedBy = d.UpdatedBy,
                UpdatedOn = d.UpdatedOn
            });
        }

        private void _fixUrls(IEnumerable<HomeSlide> slides)
        {
            foreach (var item in slides)
            {
                item.Image = FileUtils.GetUploadedFileUrl(item.Image);
            }
        }

        public virtual LoadResult GetLocalized(LoadOptions opts, string collectionId = null)
        {
            var q = collectionId == null ? Loader : QueryCollection(collectionId);
            var data = QueryLocalized(q).LoadWith(opts.GetOptionsFor<HomeSlide>());
            _fixUrls((IEnumerable<HomeSlide>)data.List);
            return data;
        }

        public override LoadResult<HomeSlide> Find(ListOptions<HomeSlide> opts)
        {
            var res = base.Find(opts);
            _fixUrls((IEnumerable<HomeSlide>)res.List);
            return res;
        }

        public override void Add(HomeSlide obj)
        {
            if (obj.ImageFile?.TmpPath != null)
            {
                obj.Image = obj.ImageFile.SaveFile("slides");
            }
            base.Add(obj);
        }

        public override void Update(HomeSlide obj)
        {
            if (obj.ImageFile?.TmpPath != null)
            {
                obj.Image = obj.ImageFile.SaveFile("slides");
            }
            base.Update(obj);
        }

        public override HomeSlide FindSingle(object id)
        {
            var i = base.FindSingle(id);
            i.ImageFile = new CodeShellCore.Files.TmpFileData { Name = i.Image?.GetAfterLast("/"), Url = i.Image };
            return i;
        }
    }
}
