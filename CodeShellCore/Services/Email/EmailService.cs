using CodeShellCore.Files;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CodeShellCore.Services.Email
{
    public class EmailService : ServiceBase
    {
        public virtual SmtpConfig Config { get; protected set; }
        public event EventHandler<MailMessage> BeforeSend;

        public EmailService()
        {
            Config = Shell.GetConfigObject<SmtpConfig>("Email");
        }

        public virtual SmtpClient CreateClient()
        {
            SmtpClient SmtpServer = new SmtpClient(Config.SmtpHost);
            SmtpServer.UseDefaultCredentials = false;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            SmtpServer.Credentials = new NetworkCredential(Config.SmtpUser, Config.SmtpPassword);
            if (Config.SmtpPort != 0)
                SmtpServer.Port = Config.SmtpPort;
            SmtpServer.EnableSsl = Config.SmtpEnableSSL;
            return SmtpServer;
        }

        public virtual MailMessage CreateMessage(string to, string subject, string body, bool isHtml = false, string display = "no-Reply")
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Config.SmtpUser, display);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isHtml;
            return mail;
        }

        public virtual Result SendEmail(SmtpClient cl, MailMessage mail)
        {
            try
            {
                if (Config.SendMails)
                {
                    if (mail.IsBodyHtml && mail.Attachments.Any())
                    {
                        for (var i = 0; i < mail.Attachments.Count; i++)
                        {
                            mail.Body = mail.Body.Replace($"%A{i}%", $"cid:{mail.Attachments[i].ContentId}");
                        }
                    }
                    BeforeSend?.Invoke(this, mail);
                    cl.Send(mail);

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

        public virtual Result SendEmail(string To, string Subject, string MsgBody, bool html = false, string displayName = "no-Reply", IEnumerable<FileBytes> files = null)
        {
            using (var cl = CreateClient())
            {
                var mail = CreateMessage(To, Subject, MsgBody, html, displayName);
                if (files != null)
                    AppendAttachments(mail, files);
                return SendEmail(cl, mail);
            }

        }

        public virtual void AppendAttachments(MailMessage message, IEnumerable<FileBytes> files)
        {
            List<Attachment> atts = new List<Attachment>();
            if (files != null)
            {
                foreach (var f in files)
                {
                    var att = new Attachment(new MemoryStream(f.Bytes), f.FileName, f.MimeType);

                    message.Attachments.Add(att);
                    f.Id = att.ContentId;
                }
            }

        }
    }
}
