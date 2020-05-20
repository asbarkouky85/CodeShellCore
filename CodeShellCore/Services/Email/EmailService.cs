using CodeShellCore.Files;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CodeShellCore.Services.Email
{
    public class EmailService : ServiceBase
    {
        public virtual SmtpConfig Config { get; protected set; }

        public EmailService()
        {
            Config = Shell.GetConfigObject<SmtpConfig>("Email");
        }

        public Result SendEmail(string To, string Subject, string MsgBody, bool html = false,string displayName="no-Reply", IEnumerable<FileBytes> files = null)
        { 
            try
            {

                if (Config.SendMails) {

                    SmtpClient SmtpServer = new SmtpClient(Config.SmtpHost);
                    SmtpServer.UseDefaultCredentials = false;

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    SmtpServer.Credentials = new NetworkCredential(Config.SmtpUser, Config.SmtpPassword);
                    if (Config.SmtpPort != 0)
                        SmtpServer.Port = Config.SmtpPort;
                    SmtpServer.EnableSsl = Config.SmtpEnableSSL;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(Config.SmtpUser, displayName);
                    mail.To.Add(To);
                    mail.Subject = Subject;
                    mail.Body = MsgBody;
                    mail.IsBodyHtml = html;

                    if (files != null)
                    {
                        foreach (var f in files)
                        {
                            var att = new Attachment(new MemoryStream(f.Bytes), f.FileName, f.MimeType);
                            mail.Attachments.Add(att);
                        }
                    }

                    SmtpServer.Send(mail);
                }
                
                return new Result(0);
            }
            catch (Exception ex)
            {
                var res = new Result(1);
                res.SetException(ex);
                return res;
            }

        }
    }
}
