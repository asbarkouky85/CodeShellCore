using System;
using System.Linq;
using CodeShellCore.Helpers;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Security
{
    public class TokenSessionManager : WebSessionManagerBase, ISessionManager
    {
        protected virtual AppClient[] Clients => new AppClient[0];
        public TokenSessionManager(IServiceProvider prov) : base(prov)
        {
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
                var currentHost = _accessor.HttpContext?.Request.GetHostUrl();
                clientId = obj;
                return obj.ExpireTime > DateTime.Now && Clients.Any(e => e.ClientId == obj.ClientId) && (string.IsNullOrEmpty(obj.Provider) || obj.Provider.ToLower() == currentHost.ToLower());
            }
            clientId = null;
            return false;
        }

        protected virtual bool ValidateUserJWT(string jwt, out JWTData userId)
        {
            if (jwt != null && jwt.TryRead(out JWTData obj))
            {
                var currentHost = _accessor.HttpContext?.Request.GetHostUrl();
                userId = obj;
                return obj.ExpireTime > DateTime.Now && (string.IsNullOrEmpty(obj.Provider) || obj.Provider.ToLower() == currentHost.ToLower());
            }
            userId = null;
            return false;
        }

        public override void AuthorizationRequest(string token)
        {
            string data = TokenToJWT(token);
            if (ValidateUserJWT(data, out JWTData user))
                SetIdentity(user);
        }

        public override void AuthorizationRequest()
        {
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
