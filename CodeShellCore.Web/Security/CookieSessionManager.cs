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

        public TimeSpan DefaultSessionTime
        {
            get { return new TimeSpan(12, 0, 0); }
        }

        public virtual void EndSession()
        {
            Accessor.HttpContext.Response.Cookies.Delete("UserId");

        }

        public override object GetCurrentUserId()
        {
            int id = 0;

            int.TryParse(Accessor.HttpContext.User.Identity.Name, out id);
            return id;
        }

        public bool IsLoggedIn()
        {
            return !GetCurrentUserId().Equals(0);
        }

        public void AuthorizationRequest()
        {
            var authCookie = Accessor.HttpContext.Request.Cookies["UserId"];

            if (authCookie != null)
            {
                try
                {
                    string jwt = Shell.Encryptor.Decrypt(authCookie);
                    JWTData data = jwt.FromJson<JWTData>();
                    if (data != null)
                        Accessor.HttpContext.User = new DefaultPrincipal(data.UserId.ToString());
                }
                catch { }

            }
        }

        public virtual void StartSession(IUser user, TimeSpan? length = null)
        {
            TimeSpan add = length ?? DefaultSessionTime;
            JWTData data = new JWTData
            {
                UserId = user.UserId,
                ExpireTime = DateTime.Now + add,
                StartTime = DateTime.Now
            };
            string st = Shell.Encryptor.Encrypt(data.ToJson());
            Accessor.HttpContext.Response.Cookies.Append("UserId", st, new CookieOptions { Expires = data.ExpireTime });
        }

        public void AuthorizationRequest(string token)
        {
            throw new NotImplementedException();
        }
    }
}
