using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CodeShellCore
{
    public class CodeShellAppOptions
    {
        public bool UseLocalization { get; set; } = false;
        public bool UseMultiTenancy { get; set; } = false;
        public bool UseJobs { get; set; } = false;
        public bool UseTransporter { get; set; } = false;
        public string DefaultCulture { get; set; } = "ar-EG";
        public IEnumerable<string> SupportedLanguages { get; set; } = new List<string> { "ar", "en" };
        public string LocalizationAssembly { get; set; }
        public string PublicRoot { get; set; } = "wwwroot";
        public string ReportsRoot { get; set; } = "Reports";
        public string SharedPathRoot { get; set; }

        public CodeShellAppOptions()
        {
            LocalizationAssembly = Shell.ProjectAssembly.GetName().Name;
        }
    }
}
