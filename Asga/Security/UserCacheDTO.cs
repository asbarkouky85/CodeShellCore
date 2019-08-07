using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Asga.Auth;

namespace Asga.Security
{
    public class UserCacheDTO
    {
        public string TenantUserId { get; set; }
        public IEnumerable<long> Customers { get; set; }
        public long? PartyId { get; set; }
        public string LogonName { get; set; }
        public int UserTypeInt { get; set; }
        public static Expression<Func<User, UserCacheDTO>> Expression
        {
            get
            {
                return e => new UserCacheDTO
                {
                    TenantUserId = e.TenantId + "_" + e.Id,
                    LogonName = e.LogonName,
                    UserTypeInt = e.UserType,
                    Customers = e.UserParties.Select(d => d.PartyId),
                    PartyId = e.CustomerId
                };
            }
        }
    }
}
