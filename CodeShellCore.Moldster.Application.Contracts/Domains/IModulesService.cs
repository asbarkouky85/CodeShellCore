using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Domains
{
    public interface IModulesService
    {
        Result InstallModule(string assemblyName, string toPath = null);
        Result UpdateModuleFiles(string assemlyName);
        IEnumerable<ModuleDTO> GetRegisteredModules();
    }
}
