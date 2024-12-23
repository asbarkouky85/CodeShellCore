using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Localization
{

    public class CustomTextRepository : MoldsterRepository<CustomText, MoldsterContext>, ICustomTextRepository
    {
        public CustomTextRepository(MoldsterContext con) : base(con)
        {
        }

        public async Task<List<CustomText>> GetBy(CustomTextRequest req)
        {
            return await Find(e => e.Locale == req.Locale && e.TenantId == req.TenantId && e.Type == req.Type);
        }

        public async Task<List<CustomText>> GetForTenant(string moduleCode)
        {
            return await Find(e => e.Tenant.Code == moduleCode);
        }
    }
}
