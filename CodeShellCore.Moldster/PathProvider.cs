using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.PageCategories.Dtos;

namespace CodeShellCore.Moldster
{
    public class DefaultPathsService : IPathsService
    {
        private string _filePath = "./appEnvironments.json";
        public virtual string CoreAppName { get; private set; }
        public virtual string LocalizationRoot { get; private set; }
        public virtual string ConfigRoot { get; private set; }
        public virtual string UIRoot { get; private set; }
        
        public virtual string UIUrl { get; private set; }
        public virtual string UILaunchProfile { get; private set; }
        public virtual List<MoldsterEnvironment> GetEnvironments()
        {
            List<MoldsterEnvironment> _envs = new List<MoldsterEnvironment>();
            if (!File.Exists(_filePath))
                throw new Exception($"appEnvironments.json is required to use this service");

            var f = File.ReadAllText(_filePath);
            if (!f.TryRead(out _envs))
                throw new Exception("appEnvironments.json is invalid");

            return _envs;
        }

        public void Dispose()
        {

        }

        public virtual List<LayoutFileDTO> GetLayouts(bool nameOnly = false)
        {
            return new List<LayoutFileDTO>();
        }

        public List<MoldsterEnvironment> UpdateEnvironments(IEnumerable<MoldsterEnvironment> envs)
        {
            var json = envs.ToJsonIndent();
            File.WriteAllText(_filePath, json);
            return envs.ToList();
        }

        public MoldsterEnvironment GetEnvironmentByName(string name)
        {
            return GetEnvironments().Where(e => e.Name == name).FirstOrDefault();
        }

        public DefaultPathsService()
        {
            var sol = Shell.SolutionFolder;
            CoreAppName = Shell.GetConfigAs<string>("Moldster:CoreAppName");
            ConfigRoot = Shell.GetConfigAs<string>("Moldster:ConfigRoot").Replace("{PARENT}", sol);
            UIRoot = Shell.GetConfigAs<string>("Moldster:UIRoot").Replace("{PARENT}", sol);
            UIUrl = Shell.GetConfigAs<string>("Moldster:UIUrl", false);
            LocalizationRoot = Shell.GetConfigAs<string>("Moldster:LocalizationRoot", false)?.Replace("{PARENT}", sol);
            UILaunchProfile = Shell.GetConfigAs<string>("Moldster:UILaunchProfile", false);
            if (LocalizationRoot == null)
                LocalizationRoot = ConfigRoot;
        }

    }
}
