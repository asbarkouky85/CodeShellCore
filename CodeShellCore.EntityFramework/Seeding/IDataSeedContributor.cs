using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Seeding
{
    public interface IDataSeedContributor
    {
        void SeedAsync(DbContext context);
    }

    public interface IDataSeedContributor<T> : IDataSeedContributor where T : DbContext
    {
        void SeedAsync(T context);
    }
}
