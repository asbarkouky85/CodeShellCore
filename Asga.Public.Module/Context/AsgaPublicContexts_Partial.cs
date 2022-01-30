using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Asga.Public
{
    public partial class AsgaPublicContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(CodeShellCore.Shell.GetConfigAs<string>("ConnectionStrings:DB"));
            base.OnConfiguring(optionsBuilder);
        }

        [DbFunction]
        public string GetLocalized(string EntityType, long? EntityId, int LocaleId, string PropertyName, string def)
        {
            throw new NotImplementedException();
        }

    }
}
