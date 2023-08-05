using CodeShellCore.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.EntityFramework
{
    public class CodeShellDbContext<T> : DbContext where T : DbContext
    {
        protected virtual string ConnectionStringKey => "Default";
        protected CurrentTenant CurrentTenant { get; private set; }

        public CodeShellDbContext(DbContextOptions<T> opts) : base(opts)
        {

        }

        public void SetCurrentTenant(CurrentTenant tenant)
        {
            CurrentTenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString;
                if (CurrentTenant?.TenantId == null)
                {
                    connectionString = Shell.GetConfigAs<string>($"ConnectionStrings:{ConnectionStringKey}");
                    if (connectionString == null)
                    {
                        connectionString = Shell.GetConfigAs<string>($"ConnectionStrings:Default");
                    }
                }
                else
                {
                    connectionString = CurrentTenant.GetConnectionString();
                }
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
