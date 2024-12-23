using CodeShellCore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Localization
{
    public interface ICustomTextRepository : IRepository<CustomText>
    {
        Task<List<CustomText>> GetForTenant(string moduleCode);
        Task<List<CustomText>> GetBy(CustomTextRequest req);
    }
}
