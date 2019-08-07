using Asga.Security;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Text;
using CodeShellCore.Web;
using CodeShellCore.Web.Security;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Web.Security
{
    public class AsgaSessionManager : TokenSessionManager
    {
        public AsgaSessionManager(IUserDataService cache,IHttpContextAccessor acc) : base(cache,acc)
        {
        }

        public override void AuthorizationRequest()
        {
            string data = GetJWTDataFromHeader();

            AuthorizationRequest(data);
        }

        public override void AuthorizationRequest(string data)
        {
            if (data != null)
            {
                AsgaJWTData jwt = data.FromJson<AsgaJWTData>();

                if (jwt != null && jwt.IsValid(_accessor.HttpContext.Request.GetHostUrl()))
                {
                    SetIdentity(jwt.UserId);
                }
            }
        }
    }
}
