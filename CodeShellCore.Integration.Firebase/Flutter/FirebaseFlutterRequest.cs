using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Integration.Firebase.Flutter
{
    public class FirebaseFlutterRequest
    {
        public List<string> registration_ids { get; set; }
        public bool content_available { get; set; } = true;
        [JsonProperty("apns-priority")]
        public int apnspriority { get; set; }
        public object data { get; set; }

    }
}
