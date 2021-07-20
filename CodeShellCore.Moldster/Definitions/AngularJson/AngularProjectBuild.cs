using System.Collections.Generic;

namespace CodeShellCore.Moldster.Definitions.AngularJson
{
    public class AngularProjectBuild
    {
        public string Builder { get; set; }
        public AngularProjectBuildOptions Options { get; set; }
        public Dictionary<string, AngularProjectBuildConfiguration> Configurations { get; set; }
    }


}
