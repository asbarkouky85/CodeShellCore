using CodeShellCore.Data.EntityFramework;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class ResourceRepository : MoldsterRepository<Resource, ConfigurationContext>,IMoldsterResourceRepository
    {
        public ResourceRepository(ConfigurationContext con) : base(con)
        {
        }

        public Resource GetResource(long domainId, string resourceName)
        {
            Resource res = FindSingle(d => d.DomainId == domainId && d.Name == resourceName);

            if (res == null)
            {
                res = new Resource
                {
                    Name = resourceName,
                    DomainId = domainId
                };
                Add(res);
            }
            return res;
        }
    }
}
