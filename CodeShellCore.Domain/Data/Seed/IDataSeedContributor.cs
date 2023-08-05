using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Seed
{
    public interface IDataSeedContributor
    {
        Task SeedAsync(DataSeedContext context);
    }
}
