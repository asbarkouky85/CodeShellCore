using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using Asga.Auth;
using Asga.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Asga.Security
{
    public class UserDTO : IAuthorizableUser
    {
        public long Id { get; set; }
        public object UserId { get; set; }
        public long TenantId { get; set; }
        public string TenantCode { get; set; }
        public string Name { get; set; }
        public string LogonName { get; set; }
        public string CustomerLogo { get; set; }
        public Dictionary<string, Permission> Permissions { get; set; }
        public int UserTypeInt { get; set; }
        public UserTypes UserType { get { return (UserTypes)UserTypeInt; } }
        public string UserTypeString { get { return UserType.ToString(); } }
        public IEnumerable<string> Apps { get; set; }
        public IEnumerable<long> Customers { get; set; }
        public long? PartyId { get; set; }
        public UserDTO()
        {
            Permissions = new Dictionary<string, Permission>();
        }

        public static Expression<Func<User, UserDTO>> FromAuthUser
        {
            get
            {
                return d => new UserDTO
                {
                    Id = d.Id,
                    UserId = d.Id,
                    LogonName = d.LogonName,
                    Name = d.FirstName + " " + d.LastName,
                    TenantId = d.TenantId.HasValue ? d.TenantId.Value : 0,
                    TenantCode = d.Tenant.Code,
                    UserTypeInt = d.UserType,
                    PartyId = d.CustomerId,
                    Apps = d.TenantAppUsers.Select(x => x.TenantApp.Name).ToList(),
                    Customers = d.UserParties.Select(x => x.PartyId).ToList()
                };
            }
        }

        

    }
}
