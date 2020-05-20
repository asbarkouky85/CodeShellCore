using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.CLI;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services.Db
{
    public class DbMoldsterService : MoldsterService
    {
        IDbTemplateProcessingService _custom => GetService<IDbTemplateProcessingService>();
        IConfigUnit _unit => GetService<IConfigUnit>();
        IScriptGenerationService _ts => GetService<IScriptGenerationService>();

        public DbMoldsterService(
            IServiceProvider prov) : base(prov)
        {

        }

        public override void ProcessTemplates(string modCode, string domain = null)
        {
            if (domain == null)
                ProcessAllTemplates(modCode);
            else
                ProcessDomainTemplates(domain, modCode);
        }

        public void ProcessAllTemplates(string modCode)
        {
            long tenantId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            var lst = _unit.PageCategoryRepository.GetValues(d => d.Id, d => d.Pages.Any(e => e.TenantId == tenantId));
            foreach (long id in lst)
            {
                _custom.ProcessForTenant(id, tenantId);
                _ts.GeneratePageCategory(id);
            }
        }

        public void ProcessDomainTemplates(string domain, string modCode)
        {
            long tenantId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            IEnumerable<long> lst = _unit.PageCategoryRepository.GetDomainTemplates(domain, tenantId);

            foreach (long id in lst)
            {
                _custom.ProcessForTenant(id, tenantId);
                _ts.GeneratePageCategory(id);
            }
        }

    }
}
