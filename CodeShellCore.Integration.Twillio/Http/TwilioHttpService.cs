using CodeShellCore.Http;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Integration.Twilio.Http
{
    public class TwilioHttpService : HttpService, ITwilioHttpService
    {
        private string _baseURl;
        protected override string BaseUrl => _baseURl;
        protected TwilioOptions Options { get; }

        public TwilioHttpService(IOptions<TwilioOptions> options)
        {
            _baseURl = options.Value.Url;
            Options = options.Value;
        }

        protected override Task ConfigureClient(HttpClient client)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{Options.UserName}:{Options.Password}");


            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return Task.CompletedTask;
        }

        public async Task<TwilioResultDto> SendMessage(TwilioRequestDto request)
        {
            return await PostUrlEncoded<TwilioResultDto>("", request);
        }


    }
}
