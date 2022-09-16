using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Asga.Auth
{
    public partial class AuthContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                var connectionString = CodeShellCore.Shell.GetConfigAs<string>("ConnectionStrings:Auth", false);
                if (!string.IsNullOrEmpty(connectionString))
                    optionsBuilder.UseSqlServer(connectionString);
            }

        }
    }
}
