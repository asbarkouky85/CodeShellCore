using Asga.Common.Data;
using Asga.Common.Services;
using Asga.Data;

namespace Asga.Auth.Data
{
    public class DomainRepository : AsgaRepository<Domain,AuthContext>, IDomainRepository
    {
        
        public DomainRepository(AuthContext con, AsgaCollectionService ser) : base(con, ser)
        {
        }

        
    }
}
