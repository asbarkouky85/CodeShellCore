using CodeShellCore.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Definitions.AngularJson
{
    public class AngularTenant
    {
        private readonly JObject _obj;
        public JObject Store => _obj;
        public AngularTenant(JObject ob)
        {
            _obj = ob;
        }

        public string OutputPath
        {
            get
            {
                return _obj.GetPathAsString("architect:build:options:outputPath");
            }
            set
            {
                _obj.SetPathValue("architect:build:options:outputPath", value);
            }
        }
        public void SetConfiguration(string name, string browserTarget, IEnumerable<AngularProjectFileReplacement> rep)
        {
            var path = _obj.CreatePathJObject("architect:build:configurations:" + name);
            var p = new AngularProjectBuildConfiguration(path);
            p.BrowserTarget = browserTarget;
            p.FileReplacements = rep;
        }

        
    }
}
