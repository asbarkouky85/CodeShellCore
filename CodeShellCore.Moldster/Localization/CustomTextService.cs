using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Extensions.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Localization
{
    public class CustomTextService : DataService<IMoldsterUnit>, ICustomTextService
    {
        private readonly ILocalizationService loc;

        public CustomTextService(IMoldsterUnit unit, ILocalizationService loc) : base(unit)
        {
            this.loc = loc;
        }

        public async Task<PagedResult<CustomTextDto>> Get(CustomTextRequestDto req, PagedListRequestDto opts)
        {
            PagedResult<CustomTextDto> data;
            if (req.ModifiedOnly)
            {
                var op = opts.GetOptionsFor<CustomText>();
                op.AddFilter(e => e.TenantId == req.TenantId);
                op.AddFilter(e => e.Type == req.Type);
                op.AddFilter(e => e.Locale == req.Locale);
                var sdata = await Unit.CustomTextRepository.Find(op);
                data = Mapper.Map(sdata, new PagedResult<CustomTextDto>());
                foreach (var x in data.List)
                    x.State = "Attached";

            }
            else
            {
                data = await loc.LoadForTenant(req, opts);
                var customTextReq = Mapper.Map(req, new CustomTextRequest());
                List<CustomText> db = await Unit.CustomTextRepository.GetBy(customTextReq);
                var lst = new List<CustomTextDto>();
                foreach (var item in data.List)
                {
                    var ex = db.Where(e => e.Code == item.Code).FirstOrDefault();
                    if (ex != null)
                    {
                        ex.State = "Attached";
                        lst.Add(Mapper.Map(ex, new CustomTextDto()));
                    }
                    else
                    {
                        lst.Add(item);
                    }
                }
                data.List = lst;
            }

            return new PagedResult<CustomTextDto>
            {
                TotalCount = data.TotalCount,
                List = Mapper.Map(data.List, new List<CustomTextDto>())
            };
        }

        public async Task<SubmitResult> SaveChanges(IEnumerable<CustomTextDto> lst)
        {
            await Unit.CustomTextRepository.ApplyChanges(lst, Mapper);
            return await Unit.SaveChanges();
        }
    }
}
