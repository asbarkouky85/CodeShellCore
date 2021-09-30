using CodeShellCore.Linq;
using CodeShellCore.Moldster.Definitions;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Builder
{
    public interface IEnvironmentsService
    {
        LoadResult<MoldsterEnvironment> Get();
        MoldsterEnvironment Post(MoldsterEnvironment dto);
        MoldsterEnvironment Put(MoldsterEnvironment env);
        IEnumerable<string> GetDatabaseList(string name);
        void Delete(string name);
    }
}
