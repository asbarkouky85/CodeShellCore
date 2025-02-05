using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.UnitTest.UnitOfWork
{

    public class TestRepository<T, TContext> : Repository_Int64<T, TContext>
        where T : class, IEntity<long>
        where TContext : DbContext
    {
        public TestRepository(TContext con) : base(con)
        {
        }
    }
}
