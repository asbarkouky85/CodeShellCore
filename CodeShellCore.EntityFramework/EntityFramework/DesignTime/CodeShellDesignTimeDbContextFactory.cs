using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.EntityFramework.DesignTime
{
    public abstract class CodeShellDesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        protected abstract string ConnectionStringKey { get; }
        public TContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var conn = configuration.GetConnectionString("Default");
            conn = configuration.GetConnectionString(ConnectionStringKey) ?? conn;

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(conn);
            return (TContext)Activator.CreateInstance(typeof(TContext), new[] { optionsBuilder.Options });
            //return new AssetsContext(optionsBuilder.Options);
        }
    }
}
