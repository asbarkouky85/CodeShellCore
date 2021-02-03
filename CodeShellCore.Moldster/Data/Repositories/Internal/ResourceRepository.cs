using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Data.Repositories.Internal
{
    public class ResourceRepository : MoldsterRepository<Resource, MoldsterContext>, IMoldsterResourceRepository
    {
        public ResourceRepository(MoldsterContext con) : base(con)
        {
        }

        public IEnumerable<string> GetByMoldsterModule(string installPath)
        {
            installPath = installPath.Replace("\\", "/");
            var q = from r in Loader
                    where
                        r.Domain.NameChain.StartsWith("/" + installPath) ||
                        r.PageCategories.Any(e => e.Domain.NameChain.StartsWith("/" + installPath))
                    select r.Name + (r.DomainId == null ? "" : ":" + r.Domain.Name);
            return q.ToList();
        }

        public Resource GetResource(string resourceName, string serviceName = null, List<Domain> doms = null)
        {
            Resource res = FindSingle(d => d.Name == resourceName && (serviceName == null || d.Domain.Name == serviceName));
            if (res == null)
            {
                res = new Resource { Id = Utils.GenerateID(), Name = resourceName };
                if (serviceName != null)
                {
                    Domain dom = DbContext.Domains.Where(d => d.Name == serviceName).FirstOrDefault();
                    if (doms != null && dom == null)
                    {
                        dom = doms.Where(d => d.Name == serviceName).FirstOrDefault();
                    }
                    if (dom == null)
                    {
                        dom = new Domain { Id = Utils.GenerateID(), Name = serviceName };
                        DbContext.Domains.Add(dom);
                        if (doms != null)
                            doms.Add(dom);
                    }
                    dom.Resources.Add(res);
                }
                else
                {
                    Add(res);
                }
            }
            return res;
        }
    }
}
