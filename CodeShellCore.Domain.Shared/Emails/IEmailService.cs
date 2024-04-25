using CodeShellCore.Files;
using CodeShellCore.Helpers;
using System.Collections.Generic;

namespace CodeShellCore.Services.Email
{
    public interface IEmailService
    {
        Result SendEmail(string To, string Subject, string MsgBody, bool html = false, string displayName = "no-Reply", IEnumerable<FileBytes> files = null);
    }
}