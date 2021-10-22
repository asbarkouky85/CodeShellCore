using CodeShellCore.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.CodeGeneration.Dtos
{
    public class AngularProjectBuildConfiguration
    {
        private readonly JObject ob;

        public AngularProjectBuildConfiguration(JObject ob)
        {
            this.ob = ob;
        }
        public IEnumerable<AngularProjectFileReplacement> FileReplacements
        {
            get { return ob.GetPathAs<List<AngularProjectFileReplacement>>("fileReplacements"); }
            set { ob.SetPathValue("fileReplacements", value); }
        }
        public string BrowserTarget
        {
            get { return ob.GetPathAsString("browserTarget"); }
            set { ob.SetPathValue("browserTarget", value); }
        }
    }
}
