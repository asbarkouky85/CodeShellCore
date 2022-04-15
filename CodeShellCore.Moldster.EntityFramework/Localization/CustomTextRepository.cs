using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Localization;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
{

    public class CustomTextRepository : MoldsterRepository<CustomText, MoldsterContext>,ICustomTextRepository
    {
        public CustomTextRepository(MoldsterContext con) : base(con)
        {
        }

        public List<CustomText> GetBy(CustomTextRequest req)
        {
            return Find(e => e.Locale == req.Locale && e.TenantId == req.TenantId && e.Type == req.Type);
        }

        public List<CustomText> GetForTenant(string moduleCode)
        {
            return Find(e => e.Tenant.Code == moduleCode);
        }
    }
}
