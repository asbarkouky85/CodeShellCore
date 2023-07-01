using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Data.EntityFramework
{
    public class Repository_Int32<T, TContext> : KeyRepository<T, TContext, int> where T : class, IModel<int> where TContext : DbContext
    {
        public Repository_Int32(TContext con) : base(con)
        {
        }

    }
}
