using CodeShellCore.Files.Reporting;
using CodeShellCore.Helpers;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.DependencyInjection
{
    public static class InjectionExtensions
    {
        public static void AddRdlcGenerator(this IServiceCollection coll)
        {
            coll.AddTransient<RdlcDataSetGenerator>();
        }

        

        public static void AddMultiTenantData<T>(this IServiceCollection coll) where T : class, ITenantDataProvider
        {

            coll.AddTransient<ITenantDataProvider, T>();
        }
    }
}
