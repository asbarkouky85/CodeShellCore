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

        public static T GetCurrentUserAs<T>(this IServiceProvider prov) where T : class, IUser
        {
            var user = prov.GetCurrentUser();
            if (user == null)
                return null;
            return (T)user;
        }


        public static bool TryGetService<T>(this IServiceProvider provider, out T service)
        {
            T ser = provider.GetService<T>();
            if (ser != null)
            {
                service = ser;
                return true;
            }

            service = Activator.CreateInstance<T>();
            return false;
        }

        public static void SetCurrentTenant(this IServiceProvider provider, long tenantId)
        {
            provider.GetRequiredService<CurrentTenant>().TenantId = tenantId;
        }

        public static void SetCurrentUser(this IServiceProvider provider, IUser user)
        {
            var acc = provider.GetService<IUserAccessor>();
            acc.Set(user);
        }

        public static void SetCurrentUserId(this IServiceProvider provider, string id, bool asClient = false)
        {
            var acc = provider.GetService<IUserAccessor>();
            acc.UserId = id;
            if (asClient)
            {
                acc.ClientId = id;
                provider.GetService<ClientData>().ClientId = id;
            }

        }

        public static IUser GetCurrentUser(this IServiceProvider provider)
        {
            return provider.GetService<IUserAccessor>().User;
        }




        public static void AddMultiTenantData<T>(this IServiceCollection coll) where T : class, ITenantDataProvider
        {
            coll.AddScoped<CurrentTenant>();
            coll.AddTransient<ITenantDataProvider, T>();
        }
    }
}
