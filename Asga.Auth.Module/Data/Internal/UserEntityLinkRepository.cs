using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security;
using System.Collections.Generic;
using System.Linq;

namespace Asga.Auth.Data
{
    public class UserEntityLinkRepository : AsgaRepository<UserEntityLink, AuthContext>, IUsersEntityLinkRepository
    {
        public UserEntityLinkRepository(AuthContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
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
