using CodeShellCore.Data.Lookups;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Services
{
    public interface IHasLookupsEntityService
    {
        Task<Dictionary<string,IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data);
        Task<Dictionary<string,IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> data);
    }
}
