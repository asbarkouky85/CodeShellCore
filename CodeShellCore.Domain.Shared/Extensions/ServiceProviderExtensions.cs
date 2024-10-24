using CodeShellCore.Helpers;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void SetCurrentTenant(this IServiceProvider provider, long tenantId)
        {
            provider.GetRequiredService<CurrentTenant>().TenantId = tenantId;
        }

        public static void SetCurrentTenantVersion(this IServiceProvider provider, string version)
        {
            provider.GetRequiredService<CurrentTenant>().Version = version;
        }

        public static long GetCurrentTenant(this IServiceProvider provider)
        {
            return provider.GetRequiredService<CurrentTenant>().TenantId;
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
    }
}
