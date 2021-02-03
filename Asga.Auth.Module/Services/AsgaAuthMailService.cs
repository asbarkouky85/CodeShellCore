using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Services
{
    public class AsgaAuthMailService : CodeShellCore.Security.Authentication.Internal.AuthenticationMailService
    {

        public override string ResetPasswordHTMLTemplate => _emailTemplate;
        private static string _emailTemplate;

        static AsgaAuthMailService()
        {
            _emailTemplate = Properties.Resources.reset_password_email;
        }
    }
}
