using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CodeShellCore.Moldster.Domains
{
    public class MoldsterModulesConfig
    {
        static List<MoldsterModule> _modules = new List<MoldsterModule>();
        static Dictionary<string, string> _paths = new Dictionary<string, string>();
        internal static IEnumerable<MoldsterModule> Modules => _modules;

        public void Register(string assemblyName, string projectPath = null)
        {
            var sol = Shell.SolutionFolder;

            var assembly = Assembly.Load(assemblyName);
            var manager = new ResourceManager($"{assemblyName}.Properties.Resources", assembly);
            byte[] obj = (byte[])manager.GetObject("Module", new System.Globalization.CultureInfo("en"));

            if (obj == null)
            {
                throw new Exception($"Unable to register {assemblyName} Module resource file is not found");
            }

            var definer = Encoding.UTF8.GetString(obj).FromJson<MoldsterModule>();
            definer.AssemblyName = assemblyName;

            if (projectPath != null)
                _paths[definer.AssemblyName] = projectPath.Replace("{PARENT}", sol);
            if (Modules.Any(d => d.AssemblyName == definer.AssemblyName))
                throw new Exception("already exists " + definer.AssemblyName);

            _modules.Add(definer);

        }

        internal static void SetModule(MoldsterModule mod)
        {
            var lst = new List<MoldsterModule>();
            foreach (var m in Modules)
            {
                if (m.AssemblyName == mod.AssemblyName)
                    lst.Add(mod);
                else
                    lst.Add(m);
            }
            _modules = lst;
        }

        public static bool GetProjectPath(string assemblyName, out string path)
        {
            return _paths.TryGetValue(assemblyName, out path);
        }
    }
}
