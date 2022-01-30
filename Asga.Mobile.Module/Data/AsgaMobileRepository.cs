using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Data
{
    public class AsgaMobileRepository<T, TContext> : KeyRepository<T, TContext, long>
        where T : class, IModel<long>
        where TContext : DbContext
    {
        public AsgaMobileRepository(TContext con) : base(con)
        {
        }
    }
}
