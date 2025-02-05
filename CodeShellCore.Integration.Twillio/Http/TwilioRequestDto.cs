using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CodeShellCore.Text;

namespace CodeShellCore.Integration.Twilio.Http
{
    public class TwilioRequestDto
    {
        public string To { get; set; }
        public string From { get; set; }
        public string ContentSid { get; set; }
        public Dictionary<string,string> ContentVariables { get; set; }

    }
}
