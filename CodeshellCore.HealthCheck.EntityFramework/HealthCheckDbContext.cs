using CodeShellCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.HealthCheck
{
    public class HealthCheckDbContext : CodeShellDbContext<HealthCheckDbContext>
    {
        public DbSet<CheckItem> CheckItems { get; set; }
        protected override string ConnectionStringKey => "HealthChecker";

        public HealthCheckDbContext(DbContextOptions<HealthCheckDbContext> opts) : base(opts)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckItem>(e =>
            {
                e.Property(e => e.ServiceName).HasMaxLength(100);
                e.Property(e => e.Host).HasMaxLength(100);
                e.Property(e => e.Response);
            });
        }
    }
}
