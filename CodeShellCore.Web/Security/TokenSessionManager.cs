using CodeShellCore.Files.Logging;
using CodeShellCore.Helpers;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Security
{
    public class TokenSessionManager : WebSessionManagerBase, ISessionManager
    {
        protected static AppClient[] Clients { get; set; } 
        public TokenSessionManager(IServiceProvider prov) : base(prov)
        {

        }

        private async Task _initClients()
        {
            if (Clients == null)
            {
                Clients = (await ServiceProvider.GetRequiredService<IClientProvider>().Get()).ToArray();
            }
        }
        public override TimeSpan DefaultSessionTime { get { return new TimeSpan(24, 0, 0); } }


        public virtual string GetTokenFromHeader()
        {
            return _accessor.HttpContext?.GetHeader(HttpHeaderKeys.Authorization);
        }

        public virtual string GetClientTokenFromHeader()
        {
            return _accessor.HttpContext?.GetHeader(HttpHeaderKeys.ClientToken);
        }

        protected virtual string TokenToJWT(string head)
        {
            _accessor.HttpContext.User = null;
            if (head != null)
            {
                string data = Shell.Encryptor.Decrypt(head);
                if (data != null)
                {
                    return data;
                }
            }
            return null;
        }

        protected virtual bool ValidateClientJWT(string jwt, out ClientJwt clientId)
        {
            if (jwt != null && jwt.TryRead(out ClientJwt obj))
            {

                var headerTenant = ReadTenantId();
                long userTenantId;
                long.TryParse(obj.TenantId, out userTenantId);
                var currentProvider = Shell.AuthServiceProvider;
                clientId = obj;
                var isValid = true;

                if (Clients.Any() && !Clients.Any(e => e.ClientId == obj.ClientId))
                {
                    Logger.WriteLine($"Invalid Client Token : Invalid Client {obj.ClientId}");
                    isValid = false;
                }

                if (obj.ExpireTime < DateTime.Now)
                {
                    Logger.WriteLine($"Invalid Client Token : Expire time {obj.ExpireTime.ToString("yyyy-MM-dd HH:mm")}");
                    isValid = false;
                }

                if (!string.IsNullOrEmpty(obj.Provider) && !string.IsNullOrEmpty(currentProvider) && obj.Provider.ToLower() != currentProvider.ToLower())
                {
                    Logger.WriteLine($"Invalid Client Token : Invalid Provider {obj.Provider}!={currentProvider}");
                    isValid = false;
                }

                if (headerTenant != null && headerTenant != 0 && headerTenant != userTenantId)
                {
                    Logger.WriteLine($"Invalid Client Token : Tenant unmatch {headerTenant}!={userTenantId}");
                    isValid = false;
                }
                return isValid;
            }
            clientId = null;
            return false;
        }

        protected virtual bool ValidateUserJWT(string jwt, out JWTData jwtResult)
        {

            if (jwt != null && jwt.TryRead(out JWTData obj))
            {
                var headerTenant = ReadTenantId();
                long userTenantId;
                long.TryParse(obj.TenantId, out userTenantId);
                var currentProvider = Shell.AuthServiceProvider;
                jwtResult = obj;
                var isValid = true;
                if (obj.ExpireTime < DateTime.Now)
                {
                    Logger.WriteLine($"Invalid Token : Expire time {obj.ExpireTime.ToString("yyyy-MM-dd HH:mm")}");
                    isValid = false;
                }

                if (!string.IsNullOrEmpty(obj.Provider) && !string.IsNullOrEmpty(currentProvider) && obj.Provider.ToLower() != currentProvider.ToLower())
                {
                    Logger.WriteLine($"Invalid Token : Invalid Provider {obj.Provider}!={currentProvider}");
                    isValid = false;
                }

                if (headerTenant != null && headerTenant != 0 && headerTenant != userTenantId)
                {
                    Logger.WriteLine($"Invalid Token : Tenant unmatch {headerTenant}!={userTenantId}");
                    isValid = false;
                }
                return isValid;
            }
            Logger.WriteLine($"Invalid Token : Unable to read JWT '{jwt}'");
            jwtResult = null;
            return false;
        }

        public override void UseToken(string token)
        {
            string data = TokenToJWT(token);
            if (ValidateUserJWT(data, out JWTData user))
                SetIdentity(user);
        }


        public override async Task AuthorizationRequest()
        {
            await _initClients();
            ReadAppVersion();
            ReadTenantId();
            string head = GetTokenFromHeader();
            string cl = GetClientTokenFromHeader();

            if (!string.IsNullOrEmpty(cl))
            {
                var clData = TokenToJWT(cl);
                if (ValidateClientJWT(clData, out ClientJwt clId))
                {
                    SetIdentity(clId);
                }
            }
            else
            {
                string data = TokenToJWT(head);
                if (ValidateUserJWT(data, out JWTData user))
                    SetIdentity(user);
            }

        }

        public virtual string CheckRefreshToken(string refreshToken)
        {
            if (refreshToken != null)
            {
                var dec = Shell.Encryptor.Decrypt(refreshToken);
                string devId = _accessor.HttpContext.RequestServices.GetService<ClientData>().DeviceId;
                if (dec.TryRead(out RefreshJWTData data))
                {
                    if (data.DeviceId == devId)
                        return data.UserId;

                }
            }
            return null;
        }




    }
}
