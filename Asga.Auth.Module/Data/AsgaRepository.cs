using CodeShellCore.Data;
using CodeShellCore.Helpers;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security;

namespace Asga.Auth.Data
{
    public class AsgaRepository<T, TContext> : CollectionRepository<T, TContext,long> 
        where T : class, IModel<long> 
        where TContext : DbContext
    {
        public AsgaRepository(TContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
        {
        }

        public override void Add(T obj)
        {
            if (obj.Id == 0)
                obj.Id = Utils.GenerateID();
            base.Add(obj);
        }
    }
}
