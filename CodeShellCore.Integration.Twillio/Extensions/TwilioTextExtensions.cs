using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Integration.Twilio.Extensions
{
    public static class TwilioTextExtensions
    {
        static Regex _numberExpression = new Regex("\\d+");
        public static string ToTwilioPhoneNumber(this string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                var matches = _numberExpression.Matches(phoneNumber);
                if (matches.Any())
                    return $"whatsapp:+{string.Join("", matches)}";
            }
            return phoneNumber;
        }
    }
}
