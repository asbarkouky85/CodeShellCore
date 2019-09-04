using CodeShellCore.Caching;

namespace CodeShellCore.Security.Authorization
{
    public class DefaultUserDataService : UserDataServiceBase, IUserDataService
    {
        private readonly ISecurityUnit unit;

        public DefaultUserDataService(ISecurityUnit unit, ICacheProvider cache) : base(cache)
        {
            this.unit = unit;
        }

        protected override IUser GetUserFromDataSource(object c)
        {
            return unit.UserRepository.GetByUserId(c);
        }

        protected override RoleCacheItem GetRoleFromDataSource(object role)
        {
            return new RoleCacheItem
            {
                RoleId = role,
                Actions = unit.ResourceRepository.GetRoleResourceActions(role),
                Resources = unit.ResourceRepository.GetRoleResources(role)
            };
        }
    }
}
