using Asga.Mobile.Business;
using CodeShellCore.Data;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Http.Pushing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asga.Mobile.Data
{
    public class AsgaMobileUnit : UnitOfWork<AsgaMobileContext>, IAsgaMobileUnit
    {
        protected override Type GenericRepositoryType => typeof(AsgaMobileRepository<,>);
        protected override bool UseChangeColumns => true;

        public IRepository<UserReminder> UserReminderRepository => GetRepositoryFor<UserReminder>();

        public IRepository<Notification> NotificationRepository => GetRepositoryFor<Notification>();

        public IRepository<UserDevice> UserDeviceRepository => GetRepositoryFor<UserDevice>();

        public IUserNotificationRepository UserNotificationRepository => GetRepository<IUserNotificationRepository>();

        public AsgaMobileUnit(IServiceProvider provider) : base(provider)
        {
        }
    }
}
