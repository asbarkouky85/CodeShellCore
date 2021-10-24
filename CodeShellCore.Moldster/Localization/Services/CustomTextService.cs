using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Localization.Services
{
    public class CustomTextService : DataService<IConfigUnit>, ICustomTextService
    {
        private readonly ILocalizationService loc;

        public CustomTextService(IConfigUnit unit, ILocalizationService loc) : base(unit)
        {
            this.loc = loc;
        }

        public LoadResult<CustomText> Get(CustomTextRequest req, LoadOptions opts)
        {
            if (req.ModifiedOnly)
            {
                var op = opts.GetOptionsFor<CustomText>();
                op.AddFilter(e => e.TenantId == req.TenantId);
                op.AddFilter(e => e.Type == req.Type);
                op.AddFilter(e => e.Locale == req.Locale);
                var data = Unit.CustomTextRepository.Find(op);
                foreach (var x in data.ListT)
                    x.State = "Attached";
                return data;
            }
            var resx = loc.LoadForTenant(req, opts);
            List<CustomText> db = Unit.CustomTextRepository.GetBy(req);
            var lst = new List<CustomText>();
            foreach (var item in resx.ListT)
            {
                var ex = db.Where(e => e.Code == item.Code).FirstOrDefault();
                if (ex != null)
                {
                    ex.State = "Attached";
                    lst.Add(ex);
                }
                else
                {
                    lst.Add(item);
                }
            }
            resx.List = lst;
            return resx;
        }

        public SubmitResult SaveChanges(IEnumerable<CustomText> lst)
        {
            Unit.CustomTextRepository.ApplyChanges(lst);
            return Unit.SaveChanges();
        }
    }
}
