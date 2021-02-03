using Asga.Mobile.Business;
using Asga.Mobile.Business.Internal;
using Asga.Mobile.Data;
using Asga.Mobile.Data.Internal;
using CodeShellCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile
{
    public static class AsgaMobileModule
    {
        public static void AddAsgaMobileData(this IServiceCollection coll, bool asDefaultModule = false)
        {
            if (asDefaultModule)
            {
                coll.AddUnitOfWork<AsgaMobileUnit, IAsgaMobileUnit>();
                coll.AddContext<AsgaMobileContext>();
            }
            else
            {
                coll.AddScoped<AsgaMobileUnit>();
                coll.AddScoped<AsgaMobileContext>();
                coll.AddScoped<IAsgaMobileUnit>(s => s.GetService<AsgaMobileUnit>());
            }
            coll.AddTransient(typeof(AsgaMobileRepository<,>));
            coll.AddRepositoryFor<UserNotification, UserNotificationRepository, IUserNotificationRepository>();
        }

        public static void AddAsgaMobileModule<T>(this IServiceCollection coll, bool asDefaultModule = false) where T : class, IModuleNotificationService
        {
            coll.AddAsgaMobileData(asDefaultModule);
            coll.AddTransient<IModuleNotificationService, T>();
            coll.AddTransient<CodeShellCore.Http.Pushing.PushNotificationService>();
            coll.AddServiceFor<Notification, NotificationService, INotificationService>();
            coll.AddServiceFor<UserDevice, UserDeviceService, IUserDeviceService>();
        }
    }
}
