using Microsoft.Extensions.DependencyInjection;

using System;

using CodeShellCore.Security.Authentication;
using CodeShellCore.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Authentication.Internal;

namespace CodeShellCore.Web.Security
{
    public class TokenAuthenticationService : PermissableAuthenticationService, IAuthenticationService
    {
        private string defaultProvider;

        protected virtual TimeSpan? TokenLifeTime => new TimeSpan(1, 0, 0, 0);

        protected virtual string TokenProvider
        {
            get { return defaultProvider; }
            private set { defaultProvider = value; }
        }
        public TokenAuthenticationService(ISecurityUnit unit, IUserDataService userData) : base(unit, userData)
        {
            defaultProvider = Shell.GetConfigAs<string>("Security:TokenProvider", false);
        }

        public override LoginResult Login(string name, string password, bool remember = false)
        {
            LoginResult res = base.Login(name, password);
            if (res.IsSuccess)
            {
                SetToken(res, remember);
            }
            return res;

        }

        public override LoginResult LoginById(string id)
        {
            LoginResult res = base.LoginById(id);
            if (res.IsSuccess)
            {
                SetToken(res);
            }
            return res;
        }

        protected virtual JWTData MakeJWT(LoginResult res)
        {

            var jwt = new JWTData
            {
                UserId = res.UserData.UserId,
                Provider = TokenProvider,
                StartTime = DateTime.Now,
                ExpireTime = TokenLifeTime == null ? DateTime.MaxValue : DateTime.Now + TokenLifeTime.Value,
                DeviceId = SecurityUnit?.ClientData.DeviceId,
                TokenId = Utils.RandomAlphabet(6, CharType.Small)
            };
            if (res.UserData is IAuthorizableUser)
                jwt.Roles = ((IAuthorizableUser)res.UserData).Roles;
            return jwt;
        }

        protected void SetToken(LoginResult res, bool remember = false)
        {
            JWTData jwt = MakeJWT(res);
            res.TokenExpiry = jwt.ExpireTime;
            res.Token = Shell.Encryptor.Encrypt(jwt.ToJson());
            if (remember)
            {
                var r = GenerateRefrehToken(jwt);
                res.RefreshToken = Shell.Encryptor.Encrypt(r.ToJson());
            }
        }

        protected virtual RefreshJWTData GenerateRefrehToken(JWTData jwt)
        {
            return new RefreshJWTData
            {
                DeviceId = jwt.DeviceId,
                TokenId = jwt.TokenId,
                UserId = jwt.UserId
            };
        }

        public static string MakeTestToken(string userId, string provider)
        {
            using (var sc = Shell.GetScope())
            {
                TimeSpan time = (sc.ServiceProvider.GetService<ISessionManager>()?.DefaultSessionTime) ?? new TimeSpan(1, 0, 0, 0);
                JWTData jwt = new JWTData
                {
                    UserId = userId,
                    Provider = provider,
                    StartTime = DateTime.Now,
                    ExpireTime = DateTime.Now + time
                };

                return Shell.Encryptor.Encrypt(jwt.ToJson());
            }

        }


    }
}
