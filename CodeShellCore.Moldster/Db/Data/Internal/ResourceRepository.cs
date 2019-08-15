using CodeShellCore.Data.EntityFramework;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class ResourceRepository : MoldsterRepository<Resource, MoldsterContext>, IMoldsterResourceRepository
    {
        public ResourceRepository(MoldsterContext con) : base(con)
        {
        }

        public Resource GetResource(string resourceName, string serviceName = null)
        {
            Resource res = FindSingle(d => d.Name == resourceName);
            return res;
        }
    }
}
