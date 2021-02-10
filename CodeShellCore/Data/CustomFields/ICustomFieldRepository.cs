using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.CustomFields
{
   public interface ICustomFieldRepository
    {
        void SaveFor<T>(long id, Dictionary<string, string> dic);
        Dictionary<string, string> LoadFor<T>(long id);
        void ReplaceFor<T>(long id, Dictionary<string, string> data);
    }
}
