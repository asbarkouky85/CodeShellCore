using Asga.Auth.Views;
using System.Collections.Generic;
using CodeShellCore.Services;


namespace Asga.Security
{
    public interface IPermissionCacheService:IServiceBase
    {
        List<ResourceV> GetResourcesByRoles(long tenantId ,List<string> ids);
        List<ResourceActionV> GetActionsByRoles(long tenantId ,List<string> ids);
        UserCacheDTO GetUserCacheDto(object userId);
        void SetUserCacheDto(object userId , UserCacheDTO userCacheDto);
        void SetPermissions(string tenantRoleId, IEnumerable<ResourceV> res);
        void SetPermissions(string tenantRoleId, IEnumerable<ResourceActionV> res);
        bool IsInCache(string key);
    }
}
