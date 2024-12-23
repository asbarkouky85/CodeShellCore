using CodeShellCore.Files;
using CodeShellCore.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Services.Email
{
    public interface IEmailService
    {
        Task<Result> SendEmail(string To, string Subject, string MsgBody, bool html = false, string displayName = "no-Reply", IEnumerable<FileBytes> files = null);
    }
}