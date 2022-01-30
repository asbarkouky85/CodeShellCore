using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security;

namespace Asga.Auth.Data
{
    public class DomainRepository : AsgaRepository<Domain, AuthContext>, IDomainRepository
    {
        public DomainRepository(AuthContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
        {
        }
    }
}
