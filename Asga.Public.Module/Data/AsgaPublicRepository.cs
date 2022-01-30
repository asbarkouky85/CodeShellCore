using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Data
{
    public class AsgaPublicRepository<T, TContext> : CollectionRepository<T, TContext, long>
        where T : class, IAsgaPublicModel
        where TContext : DbContext
    {
        public AsgaPublicRepository(TContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
        {
        }
    }
}
