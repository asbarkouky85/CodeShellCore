using CodeShellCore.Data.Lookups;
using System.Collections.Generic;

namespace CodeShellCore.Moldster
{
    public interface IMoldsterLookupService
    {
        Dictionary<string, IEnumerable<Named<object>>> Modules(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<Named<object>>> PageCategoryEdit(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<Named<object>>> PageControlList(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<Named<object>>> PageEdit(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<Named<object>>> ResourceEdit(Dictionary<string, string> data);
    }
}