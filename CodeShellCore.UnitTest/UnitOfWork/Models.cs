using Asga.Auth;
using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.UnitTest.UnitOfWork
{
    public class TestUnit : UnitOfWork<AuthContext>
    {
        public TestUnit(IServiceProvider provider) : base(provider)
        {
        }

        protected override Type GenericRepositoryType => typeof(TestRepository<,>);
    }

    public class TestRepository<T, TContext> : Repository_Int64<T, TContext>
        where T : class, IModel<long>
        where TContext : DbContext
    {
        public TestRepository(TContext con) : base(con)
        {
        }
    }
}
