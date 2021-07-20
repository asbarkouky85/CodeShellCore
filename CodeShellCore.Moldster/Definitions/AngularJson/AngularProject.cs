using Newtonsoft.Json;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Definitions.AngularJson
{
    public class AngularProjectCollection : Dictionary<string, AngularProject>
    {
        public string DefaultProject { get; set; }
        public object Cli { get; set; }
    }

    public class AngularProject
    {
        public string ProjectType { get; set; }
        public Schematics Schematics { get; set; }
        public string Root { get; set; }
        public string SourceRoot { get; set; }
        public string Prefix { get; set; }
        public AngularProjectArchitect Architect { get; set; }
    }

    public class SchematicsAngularComponent
    {
        public string Style { get; set; }
    }

    public class Schematics
    {
        [JsonProperty("@schematics/angular:component")]
        public SchematicsAngularComponent SchematicsAngularComponent { get; set; }
    }
}
