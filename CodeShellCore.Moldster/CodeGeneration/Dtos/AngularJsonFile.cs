using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.CodeGeneration.Dtos
{
    public class AngularJsonFile
    {
        private readonly JObject ob;
        public Dictionary<string, AngularTenant> Tenants { get; private set; }

        public AngularJsonFile(JObject ob)
        {
            this.ob = ob;

            var v = ob.GetValue("projects");
            var p = v.ToObject<Dictionary<string, JObject>>();

            Tenants = new Dictionary<string, AngularTenant>();
            foreach (var b in p)
            {
                var at = new AngularTenant(b.Value);
                Tenants[b.Key] = at;
            }
        }

        public AngularTenant AddTenant(string code, string template)
        {
            var v = (JObject)ob.GetValue("projects");
            var tenantJ = (JObject)JsonConvert.DeserializeObject(template);
            var ngTenant = new AngularTenant(tenantJ);
            v.Add(code, tenantJ);
            return ngTenant;
        }

        public JObject JObject => ob;
    }
}
