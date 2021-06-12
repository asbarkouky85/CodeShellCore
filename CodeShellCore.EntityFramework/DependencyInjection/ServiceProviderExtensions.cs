using CodeShellCore.Seeding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static void MigrateContext<T>(this IServiceProvider prov, bool seed = true) where T : DbContext
        {
            var molds = prov.GetRequiredService<T>();
            molds.Database.Migrate();
            if (seed)
            {
                prov.SeedFor<T>();
            }
        }

        public static void SeedFor<T>(this IServiceProvider prov) where T : DbContext
        {
            var coll = prov.GetService<DataSeederCollection<T>>();
            if (coll != null)
            {
                foreach (var t in coll.Seeders)
                {
                    var c = prov.GetRequiredService<T>();
                    var seeder = (IDataSeedContributor<T>)prov.GetRequiredService(t);
                    seeder.SeedAsync(c);
                }
            }
        }
    }
}
