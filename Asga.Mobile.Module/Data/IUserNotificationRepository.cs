using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Asga.Mobile.Business;
using CodeShellCore.Data;

namespace Asga.Mobile.Data
{
    public interface IUserNotificationRepository : IRepository<UserNotification>
    {
        IEnumerable<CountByUser> CountByUsers(IEnumerable<long> userIds, Expression<Func<UserNotification, bool>> ex = null);
    }
}