using CodeShellCore.Helpers;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace CodeShellCore.Security.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly CurrentTenant tenant;

        protected CodeShellSecurityOptions Options { get; private set; }
        public TokenGenerator(IOptions<CodeShellSecurityOptions> options, CurrentTenant tenant)
        {
            Options = options.Value;
            this.tenant = tenant;
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

        public virtual void SetToken(LoginResult res, string deviceId = null, bool remember = false)
        {
            JWTData jwt = MakeJWT(res, deviceId);
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

        protected virtual JWTData MakeJWT(LoginResult res, string deviceId)
        {

            var jwt = new JWTData
            {
                UserId = res.UserData.UserId,
                Provider = Shell.AuthServiceProvider,
                StartTime = DateTime.Now,
                ExpireTime = Options.TokenLifeTime == null ? DateTime.MaxValue : DateTime.Now + Options.TokenLifeTime.Value,
                DeviceId = deviceId,
                TokenId = Utils.RandomAlphabet(6, CharType.Small),
                TenantId = tenant.TenantId.ToString()
            };
            if (res.UserData is IAuthorizableUser)
                jwt.Roles = ((IAuthorizableUser)res.UserData).Roles;
            return jwt;
        }
    }
}
