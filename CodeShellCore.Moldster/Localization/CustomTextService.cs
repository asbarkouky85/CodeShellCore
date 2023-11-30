using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Extensions.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
    public class CustomTextService : DataService<IConfigUnit>, ICustomTextService
    {
        private readonly ILocalizationService loc;

        public CustomTextService(IConfigUnit unit, ILocalizationService loc) : base(unit)
        {
            this.loc = loc;
        }

        public LoadResult<CustomTextDto> Get(CustomTextRequestDto req, LoadOptions opts)
        {
            LoadResult<CustomText> data;
            if (req.ModifiedOnly)
            {
                var op = opts.GetOptionsFor<CustomText>();
                op.AddFilter(e => e.TenantId == req.TenantId);
                op.AddFilter(e => e.Type == req.Type);
                op.AddFilter(e => e.Locale == req.Locale);
                data = Unit.CustomTextRepository.Find(op);
                foreach (var x in data.List)
                    x.State = "Attached";

            }
            else
            {
                data = loc.LoadForTenant(req, opts);
                var customTextReq = Mapper.Map(req, new CustomTextRequest());
                List<CustomText> db = Unit.CustomTextRepository.GetBy(customTextReq);
                var lst = new List<CustomText>();
                foreach (var item in data.List)
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
                data.List = lst;
            }

            return new LoadResult<CustomTextDto>
            {
                TotalCount = data.TotalCount,
                List = Mapper.Map(data.List, new List<CustomTextDto>())
            };
        }

        public SubmitResult SaveChanges(IEnumerable<CustomTextDto> lst)
        {
            Unit.CustomTextRepository.ApplyChanges(lst, Mapper);
            return Unit.SaveChanges();
        }
    }
}
