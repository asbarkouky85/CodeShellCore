using Asga.Common.Data;
using Asga.Common.Services;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asga.Auth.Data
{
    public class UserEntityLinkRepository : AsgaRepository<UserEntityLink, AuthContext>, IUsersEntityLinkRepository
    {
        public UserEntityLinkRepository(AuthContext con, AsgaCollectionService service) : base(con, service)
        {
        }

        public Dictionary<string, IEnumerable<long>> GetUserLinks(object userId)
        {
            long id = 0;
            if (userId is string)
                long.TryParse((string)userId, out id);
            else
                id = (long)userId;

            var lst = Find(d => d.UserId == id);
            string[] keys = lst.Select(d => d.EntityName).Distinct().ToArray();
            Dictionary<string, IEnumerable<long>> ret = new Dictionary<string, IEnumerable<long>>();
            foreach (var key in keys)
            {
                ret[key] = lst.Where(d => d.EntityName == key).Select(d => d.EntityId);
            }
            return ret;

        }
    }
}
