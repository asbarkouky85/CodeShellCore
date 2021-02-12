using Asga.Mobile.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Asga.Mobile.Data.Internal
{

    public class UserNotificationRepository : AsgaMobileRepository<UserNotification, AsgaMobileContext>, IUserNotificationRepository
    {
        public UserNotificationRepository(AsgaMobileContext con) : base(con)
        {
        }

        public virtual IEnumerable<CountByUser> CountByUsers(IEnumerable<long> userIds, Expression<Func<UserNotification, bool>> ex = null)
        {
            var q = Loader;
            if (ex != null)
                q = q.Where(ex);

            var x = from n in q
                    where userIds.Contains(n.UserId)
                    group n by n.UserId into N
                    select new { UserId = N.Key, Count = N.Count() };

            return x.ToList().Select(e => new CountByUser { UserId = e.UserId, Count = e.Count }).ToList();
        }
    }
}
