using System;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Security
{
    public class TokenSessionManager : WebSessionManagerBase, ISessionManager
    {
        public TimeSpan DefaultSessionTime { get { return new TimeSpan(24, 0, 0); } }

        public TokenSessionManager()
        {
        }

        public virtual string GetTokenFromHeader()
        {
            if (Accessor.HttpContext == null)
                return null;
            return Accessor.HttpContext.GetHeader("auth-token");
        }

        protected virtual string GetJWTDataFromHeader()
        {
            Accessor.HttpContext.User = null;
            string head = GetTokenFromHeader();

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

        public virtual void AuthorizationRequest(string token)
        {
            if (token != null)
            {
                string data = Shell.Encryptor.Decrypt(token);
                if (data != null)
                {
                    JWTData jwt = data.FromJson<JWTData>();

                    if (jwt != null && jwt.IsValid(Accessor.HttpContext.Request.GetHostUrl()))
                    {
                        SetIdentity(jwt.UserId);
                    }
                }
            }
        }

        public virtual void AuthorizationRequest()
        {
            string data = GetJWTDataFromHeader();

            if (data != null)
            {
                JWTData jwt = data.FromJson<JWTData>();

                if (jwt != null && jwt.IsValid(Accessor.HttpContext.Request.GetHostUrl()))
                {
                    SetIdentity(jwt.UserId);
                    
                }
            }
        }

        public override object GetCurrentUserId()
        {
            long id;
            if (Accessor.HttpContext == null)
                return 0;
            if (Accessor.HttpContext.User != null && long.TryParse(Accessor.HttpContext.User.Identity.Name, out id))
                return id;

            return 0;
        }

        public bool IsLoggedIn()
        {
            if (GetCurrentUserId() == null)
                return false;
            return !GetCurrentUserId().Equals(0);
        }

        public object CheckRefreshToken(string refreshToken)
        {
            if (refreshToken != null)
            {
                var dec = Shell.Encryptor.Decrypt(refreshToken);
                var tok = Shell.Encryptor.Decrypt(GetTokenFromHeader());
                string devId = GetDeviceId();
                if (dec.TryRead(out RefreshJWTData data) && tok.TryRead(out JWTData tokData))
                {
                    if (data.DeviceId == tokData.DeviceId && data.DeviceId == devId && data.TokenId == tokData.TokenId)
                        return tokData.UserId;

                }
            }
            return null;
        }

        public virtual void StartSession(IUser user, TimeSpan? length = null)
        {
            UsersCache[user.UserId] = user;
        }

        public void EndSession()
        {
            ClearUserCache(GetCurrentUserId());
        }

       
    }
}
