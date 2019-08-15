using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Db.Data;

namespace CodeShellCore.Moldster.Db.Services
{
    public class TenantsService : EntityService<Tenant>
    {
        public TenantsService(IConfigUnit unit) : base(unit)
        {
            Unit = unit;
        }

        IConfigUnit Unit;

        
    }
}
