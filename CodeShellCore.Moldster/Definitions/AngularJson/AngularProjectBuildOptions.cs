using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Definitions.AngularJson
{
    public class AngularProjectBuildOptions
    {
        public string OutputPath { get; set; }
        public string Index { get; set; }
        public string Main { get; set; }
        public string Polyfills { get; set; }
        public object TsConfig { get; set; }
        public bool Aot { get; set; }
        public List<string> AllowedCommonJsDependencies { get; set; }
        public List<string> Assets { get; set; }
        public List<AngularProjectBundle> Styles { get; set; }
        public List<object> Scripts { get; set; }
        public string BrowserTarget { get; set; }
        public List<string> Exclude { get; set; }
    }


}
