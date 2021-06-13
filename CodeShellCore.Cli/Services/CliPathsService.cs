﻿using CodeShellCore.Cli.Routing;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Services
{
    public class CliPathsService : IPathsService
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
            var envPath = Path.Combine(CliDispatchShell.ConfigurationApiPath, "appEnvironments.json");
            if (!File.Exists(envPath))
                throw new Exception("appEnvironments.json is required to use this service");

            var f = File.ReadAllText(envPath);
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

        public CliPathsService()
        {
            var sol = CliDispatchShell.ConfigurationApiPath.Replace("\\", "/").GetBeforeLast("/");
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