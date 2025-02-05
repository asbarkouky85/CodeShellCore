using System.Threading.Tasks;

namespace CodeShellCore.Integration.Twilio.Http
{
    public interface ITwilioHttpService
    {
        Task<TwilioResultDto> SendMessage(TwilioRequestDto request);
    }
}