using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
    public interface ICustomTextRepository : IRepository<CustomText>
    {
        List<CustomText> GetForTenant(string moduleCode);
        List<CustomText> GetBy(CustomTextRequest req);
    }
}
