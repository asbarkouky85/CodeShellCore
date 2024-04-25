using CodeShellCore.Data.Lookups;
using System.Collections.Generic;

namespace CodeShellCore.Moldster
{
    public interface IMoldsterLookupService
    {
        Dictionary<string, IEnumerable<NamedDto<object>>> Modules(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<NamedDto<object>>> PageCategoryEdit(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<NamedDto<object>>> PageControlList(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<NamedDto<object>>> PageEdit(Dictionary<string, string> data);
        Dictionary<string, IEnumerable<NamedDto<object>>> ResourceEdit(Dictionary<string, string> data);
    }
}