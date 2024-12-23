using CodeShellCore.Linq;
using CodeShellCore.Moldster.Environments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Environments.Services
{
    public interface IEnvironmentsService
    {
        Task<PagedResult<MoldsterEnvironment>> Get();
        Task<MoldsterEnvironment> Post(MoldsterEnvironment dto);
        Task<MoldsterEnvironment> Put(MoldsterEnvironment env);
        Task<IEnumerable<string>> GetDatabaseList(string name);
        Task Delete(string name);
    }
}
