using System;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Web.Security
{
    public class TokenSessionManager : WebSessionManagerBase, ISessionManager
    {

        public override TimeSpan DefaultSessionTime { get { return new TimeSpan(24, 0, 0); } }

        public TokenSessionManager(IHttpContextAccessor context) : base(context)
        {
        }

        public virtual string GetTokenFromHeader()
        {
            if (_accessor.HttpContext == null)
                return null;
            return _accessor.HttpContext.GetHeader(HttpHeaderKeys.Authorization);
        }

        protected virtual string GetJWTDataFromHeader()
        {
            _accessor.HttpContext.User = null;
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

        public override void AuthorizationRequest(string token)
        {
            if (IsProccessed(_accessor.HttpContext))
                return;
            if (token != null)
            {
                string data = Shell.Encryptor.Decrypt(token);
                if (data != null)
                {
                    JWTData jwt = data.FromJson<JWTData>();
                    var h = _accessor.HttpContext.Request.GetHostUrl();
                    if (jwt != null && jwt.IsValid(h))
                    {
                        SetIdentity(jwt.UserId);
                    }
                }
            }
            SetProccessed(_accessor.HttpContext);
        }

        public override void AuthorizationRequest()
        {
            if (IsProccessed(_accessor.HttpContext))
                return;

            string data = GetJWTDataFromHeader();

            if (data != null)
            {
                JWTData jwt = data.FromJson<JWTData>();

                if (jwt != null && jwt.IsValid(_accessor.HttpContext.Request?.GetHostUrl()))
                {
                    SetIdentity(jwt.UserId);
                }
            }
            SetProccessed(_accessor.HttpContext);
        }

        public override string GetCurrentUserId()
        {
            return _accessor.HttpContext?.User?.Identity.Name;


        }

        public override bool IsLoggedIn()
        {
            if (GetCurrentUserId() == null)
                return false;
            return !GetCurrentUserId().Equals(0);
        }

        public string CheckRefreshToken(string refreshToken)
        {
            if (refreshToken != null)
            {
                var dec = Shell.Encryptor.Decrypt(refreshToken);
                string devId = GetDeviceId();
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
