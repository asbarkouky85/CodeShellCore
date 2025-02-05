using CodeShellCore.Files;
using CodeShellCore.Integration.Twilio.Extensions;
using CodeShellCore.Integration.Twilio.Http;
using CodeShellCore.Notifications;
using CodeShellCore.Notifications.Senders;
using CodeShellCore.Services;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Integration.Twilio
{
    public class TwilioNotificationSender : INotificationSender
    {
        ITwilioHttpService service;
        private readonly IOptions<TwilioOptions> options;

        public TwilioNotificationSender(ITwilioHttpService service, IOptions<TwilioOptions> options)
        {
            this.service = service;
            this.options = options;
        }

        public NotificationProviders ProviderId => NotificationProviders.WhatsApp;



        public async Task<MessageDeliveryResult> SendAsync(NotificationMessageDeliveryDto deliveryData)
        {
            var result = new MessageDeliveryResult(true);
            try
            {
                if (!string.IsNullOrEmpty(deliveryData.User.PhoneNumber))
                {
                    var req = new TwilioRequestDto
                    {
                        ContentSid = deliveryData.TemplateCode,
                        ContentVariables = deliveryData.Parameters.FromJson<Dictionary<string,string>>(),
                        From = options.Value.FromNumber.ToTwilioPhoneNumber(),
                        To = deliveryData.User.PhoneNumber.ToTwilioPhoneNumber()
                    };
                    result.RequestContent = req.ToJson();
                    var response = await service.SendMessage(req);
                    result.ResponseContent = response.ToJson();
                }

                return result;
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }
            return result;

        }
    }
}
