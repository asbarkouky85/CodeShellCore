using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class PathProvider
    {
        public virtual string CoreAppName { get; private set; }
        public virtual string LocalizationRoot { get; private set; }
        public virtual string ConfigRoot { get; private set; }
        public virtual string UIRoot { get; private set; }
        public virtual string ConfigUrl { get; private set; }

        public PathProvider()
        {
            var sol = AppDomain.CurrentDomain.BaseDirectory.GetBeforeFirst("\\" + Shell.ProjectAssembly.GetName().Name);
            CoreAppName = Shell.GetConfigAs<string>("Moldster:CoreAppName");
            ConfigRoot = Shell.GetConfigAs<string>("Moldster:ConfigRoot").Replace("{PARENT}", sol);
            UIRoot = Shell.GetConfigAs<string>("Moldster:UIRoot").Replace("{PARENT}", sol);
            ConfigUrl = Shell.GetConfigAs<string>("Moldster:ConfigUrl");
            LocalizationRoot = Shell.GetConfigAs<string>("Moldster:LocalizationRoot", false)?.Replace("{PARENT}", sol);
            if (LocalizationRoot == null)
                LocalizationRoot = ConfigRoot;
        }

    }
}
