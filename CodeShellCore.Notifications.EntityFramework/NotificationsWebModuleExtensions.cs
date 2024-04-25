using Codeshell.Abp.EntityFrameworkCore.Devices;
using CodeShellCore.EntityFramework;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Notifications.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Notifications
{
    public static class NotificationsEntityFrameworkModuleExtensions
    {
        public static void AddCodeShellNotificationsEntityFramework<TDbContext>(this IServiceCollection coll) where TDbContext : CodeShellDbContext<TDbContext>, IDevicesDbContext
        {
            coll.AddRepositoryFor<UserDevice, UserDeviceRepository<TDbContext>, IUserDeviceRepository>();
            coll.AddUnitOfWork<CodeShellNotificationsUnit<TDbContext>, ICodeShellNotificationsUnit>(false);
        }

    }
}
