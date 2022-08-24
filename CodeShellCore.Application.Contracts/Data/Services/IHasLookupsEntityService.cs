using CodeShellCore.Data.Lookups;
using System.Collections;
using System.Collections.Generic;

namespace CodeShellCore.Data.Services
{
    public interface IHasLookupsEntityService
    {
        Dictionary<string, IEnumerable<Named<object>>> GetEditLookups(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<Named<object>>> GetListLookups(Dictionary<string, string> data);
    }
}
