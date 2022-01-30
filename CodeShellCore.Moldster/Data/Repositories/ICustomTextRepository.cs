using CodeShellCore.Data;
using CodeShellCore.Moldster.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface ICustomTextRepository : IRepository<CustomText>
    {
        List<CustomText> GetForTenant(string moduleCode);
        List<CustomText> GetBy(CustomTextRequest req);
    }
}
