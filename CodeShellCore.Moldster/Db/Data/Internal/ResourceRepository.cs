using CodeShellCore.Data.EntityFramework;
using System.Linq;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class ResourceRepository : MoldsterRepository<Resource, MoldsterContext>, IMoldsterResourceRepository
    {
        public ResourceRepository(MoldsterContext con) : base(con)
        {
        }

        public Resource GetResource(string resourceName, string serviceName = null)
        {
            Resource res = FindSingle(d => d.Name == resourceName && (serviceName == null || d.Domain.Name == serviceName));
            if (res == null)
            {
                res = new Resource { Name = resourceName };
                if (serviceName != null)
                {
                    Domain dom = DbContext.Domains.Where(d => d.Name == serviceName).FirstOrDefault();
                    if (dom == null)
                    {
                        dom = new Domain { Name = serviceName };
                        DbContext.Domains.Add(dom);
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
