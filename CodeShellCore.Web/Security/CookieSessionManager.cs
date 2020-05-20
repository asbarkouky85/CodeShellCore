using System;

using CodeShellCore.Text;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Web.Security
{
    public class CookieSessionManager : WebSessionManagerBase, ISessionManager
    {
        public CookieSessionManager(IHttpContextAccessor context) : base(context)
        {
        }

        public override TimeSpan DefaultSessionTime
        {
            get { return new TimeSpan(12, 0, 0); }
        }

        public override void EndSession()
        {
            _accessor.HttpContext.Response.Cookies.Delete("UserId");

        }

        public override object GetCurrentUserId()
        {
            int id = 0;

            int.TryParse(_accessor.HttpContext.User.Identity.Name, out id);
            return id;
        }

        public override bool IsLoggedIn()
        {
            return !GetCurrentUserId().Equals(0);
        }

        public override void AuthorizationRequest()
        {
            var authCookie = _accessor.HttpContext.Request.Cookies["UserId"];

            if (authCookie != null)
            {
                try
                {
                    string jwt = Shell.Encryptor.Decrypt(authCookie);
                    JWTData data = jwt.FromJson<JWTData>();
                    if (data != null)
                        _accessor.HttpContext.User = new DefaultPrincipal(data.UserId.ToString());
                }
                catch { }

            }
        }

        public override void StartSession(IUser user, TimeSpan? length = null)
        {
            base.StartSession(user, length);
            TimeSpan add = length ?? DefaultSessionTime;
            JWTData data = new JWTData
            {
                UserId = user.UserId,
                ExpireTime = DateTime.Now + add,
                StartTime = DateTime.Now
            };
            string st = Shell.Encryptor.Encrypt(data.ToJson());
            _accessor.HttpContext.Response.Cookies.Append("UserId", st, new CookieOptions { Expires = data.ExpireTime });
        }

        public override void AuthorizationRequest(string token)
        {
            AuthorizationRequest();
        }
    }
}
