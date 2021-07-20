using Newtonsoft.Json;

namespace CodeShellCore.Moldster.Definitions.AngularJson
{
    public class AngularProjectArchitect
    {
        public AngularProjectBuild Build { get; set; }
        public AngularProjectBuild Serve { get; set; }

        [JsonProperty("extract-i18n")]
        public AngularProjectBuild ExtractI18n { get; set; }
        public AngularProjectBuild Lint { get; set; }
    }


}
