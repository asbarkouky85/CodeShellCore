using CodeShellCore.Data.Lookups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster
{
    public interface IMoldsterLookupService : ILookupsService
    {
        Task<Dictionary<string, IEnumerable<Named<object>>>> Modules(Dictionary<string, string> data);
        Task<Dictionary<string, IEnumerable<Named<object>>>> PageCategoryEdit(Dictionary<string, string> data);
        Task<Dictionary<string, IEnumerable<Named<object>>>> PageControlList(Dictionary<string, string> data);
        Task<Dictionary<string, IEnumerable<Named<object>>>> PageEdit(Dictionary<string, string> data);
        Task<Dictionary<string, IEnumerable<Named<object>>>> ResourceEdit(Dictionary<string, string> data);
    }
}