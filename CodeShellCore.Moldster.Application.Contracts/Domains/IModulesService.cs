using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Domains
{
    public interface IModulesService
    {
        Task<Result> InstallModule(string assemblyName, string toPath = null);
        Task<Result> UpdateModuleFiles(string assemlyName);
        Task<IEnumerable<ModuleDTO>> GetRegisteredModules();
    }
}
