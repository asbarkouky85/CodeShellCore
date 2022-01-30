using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Moldster.Definitions
{
    public class PageConfig
    {
        public string Name { get; set; }
        public string ViewPath { get; set; }
        public string ComponentPath { get { return ViewPath.GetBeforeLast("/") + "/" + Name; } }
        public string RouteParameters { get; set; }
        public string ResourceActionName { get; set; }
        public string ResourcePrivilege { get; set; }
        public bool AppearsInNavigation { get; set; }
        public string ResourceName { get; set; }
        public string BaseScript { get; set; }
        public bool NoRoute { get; set; }
        public bool Entry { get; set; }
        public string Layout { get; set; }
        public string DomainName { get; set; }

        public string Registration { get { return "Registry.Register(\"" + ComponentPath + "\", " + Name + ");\n"; } }

        public object Apps { get; internal set; }

        public string GetImportString(bool removeDomain = false)
        {

            string path = ComponentPath;
            if (removeDomain)
            {
                Regex reg = new Regex("^" + DomainName + "/");
                path = reg.Replace(ComponentPath, "");
            }
            return $"import {{ {Name} }} from \"./{path}\";\n";
        }

    }
}
