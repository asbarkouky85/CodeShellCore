﻿using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class DefaultPathsService : IPathsService
    {
        public virtual string CoreAppName { get; private set; }
        public virtual string LocalizationRoot { get; private set; }
        public virtual string ConfigRoot { get; private set; }
        public virtual string UIRoot { get; private set; }
        public virtual string ConfigUrl { get; private set; }
        public virtual string UIUrl { get; private set; }
        public virtual string UILaunchProfile { get; private set; }
        public virtual List<MoldsterEnvironment> GetEnvironments()
        {
            List<MoldsterEnvironment> _envs = new List<MoldsterEnvironment>();
            if (!File.Exists("./appEnvironments.json"))
                throw new Exception("appEnvironments.json is required to use this service");

            var f = File.ReadAllText("./appEnvironments.json");
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

        public DefaultPathsService()
        {
            var sol = Shell.SolutionFolder;
            CoreAppName = Shell.GetConfigAs<string>("Moldster:CoreAppName");
            ConfigRoot = Shell.GetConfigAs<string>("Moldster:ConfigRoot").Replace("{PARENT}", sol);
            UIRoot = Shell.GetConfigAs<string>("Moldster:UIRoot").Replace("{PARENT}", sol);
            ConfigUrl = Shell.GetConfigAs<string>("Moldster:ConfigUrl");
            UIUrl = Shell.GetConfigAs<string>("Moldster:UIUrl", false);
            LocalizationRoot = Shell.GetConfigAs<string>("Moldster:LocalizationRoot", false)?.Replace("{PARENT}", sol);
            UILaunchProfile = Shell.GetConfigAs<string>("Moldster:UILaunchProfile", false);
            
        }

    }
}
