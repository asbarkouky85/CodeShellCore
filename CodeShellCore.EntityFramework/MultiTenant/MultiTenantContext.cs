using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public abstract class MultiTenantContext : DbContext
    {
        public string ConnectionString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                UseConnectionString(optionsBuilder, ConnectionString);
        }

        protected abstract void UseConnectionString(DbContextOptionsBuilder builder, string str);
    }

}
