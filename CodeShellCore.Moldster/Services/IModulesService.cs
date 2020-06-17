using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface IModulesService
    {
        Result InstallModule(string assemblyName, string toPath = null);
        Result UpdateModuleFiles(string assemlyName);
        IEnumerable<ModuleDTO> GetRegisteredModules();
    }
}
