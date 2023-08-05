using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Seed
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}
