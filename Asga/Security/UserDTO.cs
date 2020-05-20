using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using Asga.Enumerations;
using System.Collections.Generic;

namespace Asga.Security
{
    public class UserDTO : IAuthorizableUser,IEntityLinkedUser
    {
        public long Id { get; set; }
        public object UserId { get; set; }
        public long TenantId { get; set; }

        public string TenantCode { get; set; }
        public string Name { get; set; }
        public string LogonName { get; set; }
        public string Photo { get; set; }
        
        public int UserTypeInt { get; set; }
        public UserTypes UserType { get { return (UserTypes)UserTypeInt; } }
        public string UserTypeString { get { return UserType.ToString(); } }
        public string App { get; set; }
        public long? PersonId { get; set; }

        public IEnumerable<object> Roles { get; set; }
        public IEnumerable<string> Apps { get; set; }
        public Dictionary<string, DataAccessPermission> Permissions { get; set; }
        public Dictionary<string, IEnumerable<long>> EntityLinks { get; set; }
        public UserDTO()
        {
            Permissions = new Dictionary<string, DataAccessPermission>();
            EntityLinks = new Dictionary<string, IEnumerable<long>>();
        }
    }
}
