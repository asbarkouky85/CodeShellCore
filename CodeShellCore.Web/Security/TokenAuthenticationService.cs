using CodeShellCore.Security.Cryptography;
using CodeShellCore.Data;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Security.Authentication;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Helpers;
using CodeShellCore.Security.Sessions;

namespace CodeShellCore.Web.Security
{
    public class TokenAuthenticationService : DefaultAuthenticationService, IAuthenticationService
    {
        private string defaultProvider;
        private readonly ISessionManager _sessionManager;

        protected virtual string TokenProvider
        {
            get { return defaultProvider; }
            private set { defaultProvider = value; }
        }
        public TokenAuthenticationService(ISecurityUnit unit,ISessionManager sessionManager) : base(unit)
        {
            defaultProvider = Shell.GetConfigAs<string>("Security:TokenProvider", false);
            _sessionManager = sessionManager;
        }
        public override LoginResult Login(string name, string password)
        {
            LoginResult res = base.Login(name, password);
            if (res.Success)
            {
                SetToken(res);
            }

            return res;

        }

        public override LoginResult LoginById(object id)
        {
            LoginResult res = base.LoginById(id);
            if (res.Success)
            {
                SetToken(res);
            }
            return res;
        }

        protected virtual JWTData MakeJWT(LoginResult res)
        {
            TimeSpan time = new TimeSpan(1, 0, 0, 0);
            return new JWTData
            {
                Name = res.UserData.Name,
                UserId = res.UserData.UserId,
                Provider = TokenProvider,
                StartTime = DateTime.Now,
                ExpireTime = DateTime.Now + time,
                DeviceId = _sessionManager.GetDeviceIdIfWeb(),
                TokenId = Utils.RandomAlphabet(6, 2)
            };
        }

        protected void SetToken(LoginResult res)
        {
            JWTData jwt = MakeJWT(res);
            res.TokenExpiry = jwt.ExpireTime;
            res.Token = Shell.Encryptor.Encrypt(jwt.ToJson());
            RefreshJWTData r = new RefreshJWTData
            {
                DeviceId = jwt.DeviceId,
                TokenId = jwt.TokenId
            };
            res.RefreshToken = Shell.Encryptor.Encrypt(r.ToJson());
        }

        public static string MakeTestToken(object userId, string provider)
        {
            using (var sc = Shell.GetScope())
            {
                TimeSpan time = (sc.ServiceProvider.GetService<ISessionManager>()?.DefaultSessionTime)??new TimeSpan(1,0,0,0);
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
