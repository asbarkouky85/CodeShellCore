using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Caching.RedisCaching.RedisCachingService;
using CodeShellCore.Files.Logging;
using CodeShellCore.MQ;
using CodeShellCore.Services;
using Asga.Auth;
using Asga.Auth.Views;


namespace Asga.Security
{
    public class PermissionCacheService : ServiceBase , IPermissionCacheService
    {
        private static List<TenantCacheDto> _tenantCacheDtos;
        private const string TenantCacheKey = "TenantCacheDto";
        private const string ResourceVKey = "resourceV:";
        private const string ResourceActionVKey = "resourceActionsV:";
        private const string UserKey = "User:";
        private readonly IRedisCacheService _redisCacheService;

        public PermissionCacheService(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
            if (_tenantCacheDtos == null)
            {
                if (!redisCacheService.IsInCache(TenantCacheKey))
                {
                    _tenantCacheDtos = new List<TenantCacheDto>();
                    SaveTenants();
                }
                else
                {
                    _tenantCacheDtos = _redisCacheService.Get<List<TenantCacheDto>>(TenantCacheKey);
                }
            }
        }
        public List<ResourceV> GetResourcesByRoles(long tenantId, List<string> ids)
        {
            CheckTenant(tenantId);
            var recourcesIds = ids.Select(x => ResourceVKey + tenantId.ToString() + "_" + x).ToList();
            var result = _redisCacheService.GetByIdsList<ResourceV>(recourcesIds).ToList();
            return result;
        }

        

        public List<ResourceActionV> GetActionsByRoles(long tenantId, List<string> ids)
        {
            CheckTenant(tenantId);
            var resourceActionsIds = ids.Select(x => ResourceActionVKey + tenantId.ToString() + "_" + x).ToList();
            var result = _redisCacheService.GetByIdsList<ResourceActionV>(resourceActionsIds).ToList();
            return result;
        }

        public UserCacheDTO GetUserCacheDto(object userId)
        {
            var userCacheDto = _redisCacheService.Get<UserCacheDTO>(userId.ToString());
            return userCacheDto;
        }

        public void SetUserCacheDto(object userId, UserCacheDTO userCacheDto)
        {
            _redisCacheService.Set(UserKey + userId, userCacheDto);
        }

        public void SetPermissions(string tenantRoleId, IEnumerable<ResourceV> res)
        {
            _redisCacheService.Set(ResourceVKey + tenantRoleId, res);
        }

        public void SetPermissions(string tenantRoleId, IEnumerable<ResourceActionV> res)
        {
            _redisCacheService.Set(ResourceActionVKey + tenantRoleId, res);
        }

        public bool IsInCache(string key)
        {
            var result = _redisCacheService.IsInCache(key);
            return result;
        }

        public void CheckTenant(long tenantId)
        {
            
            try
            {
                var ten = _tenantCacheDtos.FirstOrDefault(d => d.TenantId == tenantId);
                if (ten == null)
                {
                    ten = new TenantCacheDto
                    {
                        TenantId = tenantId,
                        IsSync = false,
                        LastUpdate = Convert.ToDateTime("01 / 01 / 2000")
                    };
                    _tenantCacheDtos.Add(ten);
                    SaveTenants();
                }

                
            }
            catch (Exception e)
            {

                Logger.WriteException(e);
            }
        }


        protected void SaveTenants()
        {
            _redisCacheService.Set(TenantCacheKey, _tenantCacheDtos);
        }
    }
}
