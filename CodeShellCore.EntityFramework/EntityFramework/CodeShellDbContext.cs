using CodeShellCore.EntityFramework.DesignTime;
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
                if (CurrentTenant == null || CurrentTenant.TenantId == 0)
                {
                    connectionString = Shell.GetConfigAs<string>($"ConnectionStrings:{ConnectionStringKey}", false);
                    if (connectionString == null)
                    {
                        connectionString = Shell.GetConfigAs<string>($"ConnectionStrings:Default", false);
                    }
                }
                else
                {
                    connectionString = CurrentTenant.GetConnectionString();
                }
                
                if (connectionString != null && DesignTimeMigrationsAssemblies.Store.TryGetValue(typeof(T).Name, out string assembly))
                {
                    optionsBuilder.UseSqlServer(connectionString, e => e.MigrationsAssembly(assembly));
                }
                else if (connectionString != null)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else
                {
                    
                }
            }
        }
    }
}
