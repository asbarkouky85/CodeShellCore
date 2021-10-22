using CodeShellCore.Json;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.CodeGeneration.Dtos;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public class AngularJsonService : MoldsterFileHandlingService, IAngularJsonService
    {
        IConfigUnit unit => GetService<IConfigUnit>();
        public AngularJsonService(IServiceProvider provider) : base(provider)
        {
        }

        public virtual AngularJsonFile ReadFile()
        {
            var angularJsonPath = Path.Combine(Paths.UIRoot, "angular.json");
            var txt = File.ReadAllText(angularJsonPath);
            var ob = JObject.Parse(txt);
            return new AngularJsonFile(ob);
        }

        public virtual void UpdateFileFromDatabase()
        {
            var dbTenants = unit.TenantRepository.FindAs(e => new { e.Code, e.IsActive });

            var angularTenants = ReadFile();

            foreach (var t in dbTenants)
            {
                if (!angularTenants.Tenants.TryGetValue(t.Code, out AngularTenant ngt))
                {
                    var tenantConfig = Writer.FillStringParameters(Molds.AngularJsonProject, new AppComponentModel { Name = t.Code });
                    angularTenants.AddTenant(t.Code, tenantConfig);
                }
            }
            var txt = angularTenants.JObject.ToString(Formatting.Indented);
            File.WriteAllText(Path.Combine(Paths.UIRoot, "angular.json"), txt);
        }
    }
}
