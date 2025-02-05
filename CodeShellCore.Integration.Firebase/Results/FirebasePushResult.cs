using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Integration.Firebase.Results
{
    public class FirebasePushResult : Result
    {
        public override bool IsSuccess => !(success == 0 && failure == 1);
        public string multicast_id { get; set; }
        public string message_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }

        public IEnumerable<FirebaseResultItem> results { get; set; }
    }
}
