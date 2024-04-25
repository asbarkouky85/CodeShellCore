using CodeShellCore.DependencyInjection;
using CodeShellCore.MultiTenant;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Seed
{
    public class DataSeeder : IDataSeeder
    {
        private readonly IServiceProvider serviceProvider;
        private readonly CodeshellDataSeedOptions _options;

        public DataSeeder(IServiceProvider serviceProvider, IOptions<CodeshellDataSeedOptions> opts)
        {
            this.serviceProvider = serviceProvider;
            _options = opts.Value;
        }

        public async Task SeedAsync()
        {

            //using (var sc = Shell.GetScope())
            //{
            //    foreach (var seederType in _options.DataSeeders)
            //    {
            //        var seeder = (IDataSeedContributor)serviceProvider.GetService(seederType);
            //        await seeder.SeedAsync(new DataSeedContext());
            //    }
            //}

            if (Shell.UseMultiTenancy)
            {
                var provider = serviceProvider.GetRequiredService<ITenantDataProvider>();
                var tenants = provider.GetContectionStringDictionary();

                foreach (var tenant in tenants)
                {
                    using (var sc = Shell.GetScope())
                    {
                        var code = provider.GetTenantCode(tenant.Key);
                        sc.ServiceProvider.SetCurrentTenant(tenant.Key);
                        foreach (var seederType in _options.DataSeeders)
                        {
                            var seeder = (IDataSeedContributor)sc.ServiceProvider.GetRequiredService(seederType);
                            await seeder.SeedAsync(new DataSeedContext(tenant.Key, code));
                        }
                    }
                }
            }


        }
    }
}
