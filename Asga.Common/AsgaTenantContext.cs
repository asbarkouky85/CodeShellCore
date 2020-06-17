using Asga.Security;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga
{
    public abstract class AsgaTenantContext : DbContext
    {
        public string ConnectionString { get; set; }

        public AsgaTenantContext(DbContextOptions options) : base(options)
        {
        }

        public AsgaTenantContext() { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine(this);
            optionsBuilder.UseSqlServer(ConnectionString);
            //optionsBuilder.UseLoggerFactory()
        }

        
    }
}
