using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Types;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Data.EntityFramework
{
    public class Repository_Int64<T, TContext> : KeyRepository<T, TContext,long>
        where T : class, IEntity<long>
        where TContext : DbContext
    {

        public Repository_Int64(TContext con) : base(con)
        {
        }
    }
}
