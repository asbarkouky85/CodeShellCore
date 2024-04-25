using CodeShellCore.Data.Lookups;
using System.Collections;
using System.Collections.Generic;

namespace CodeShellCore.Data.Services
{
    public interface IHasLookupsEntityService
    {
        Dictionary<string, IEnumerable<NamedDto<object>>> GetEditLookups(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<NamedDto<object>>> GetListLookups(Dictionary<string, string> data);
    }
}
