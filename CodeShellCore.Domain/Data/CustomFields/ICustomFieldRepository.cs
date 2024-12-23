using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.CustomFields
{
    public interface ICustomFieldRepository
    {
        Task SaveFor<T>(long id, Dictionary<string, string> dic);
        Task<Dictionary<string, string>> LoadFor<T>(long id);
        Task ReplaceFor<T>(long id, Dictionary<string, string> data);
    }
}
