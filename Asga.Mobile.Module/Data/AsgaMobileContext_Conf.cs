using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Asga.Mobile
{
    public partial class AsgaMobileContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(CodeShellCore.Shell.GetConfigAs<string>("ConnectionStrings:DB"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
