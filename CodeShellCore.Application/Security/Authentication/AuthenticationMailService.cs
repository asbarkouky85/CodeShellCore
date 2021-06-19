using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Services.Email;

namespace CodeShellCore.Security.Authentication.Internal
{
    public abstract class AuthenticationMailService : EmailService, IAuthenticationMailService
    {
        protected readonly WriterService Writer;

        public AuthenticationMailService()
        {
            Writer = new WriterService();
        }
        public abstract string ResetPasswordHTMLTemplate { get; }
        public virtual Result SendResetEmail(ResetPasswordDTO dto)
        {
            var body = Writer.FillStringParameters(ResetPasswordHTMLTemplate, dto);
            var c = CreateClient();
            var mess = CreateMessage(dto.Email, "Your Password was Reset", body, true, Config.SenderName ?? "no-reply");
            return SendEmail(c, mess);
        }


    }
}
