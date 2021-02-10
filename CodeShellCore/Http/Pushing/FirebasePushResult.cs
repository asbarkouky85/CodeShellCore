using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Http.Pushing
{
    public class FirebasePushResult : Result
    {
        public override bool IsSuccess => success == 1;
        public string multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }
        public IEnumerable<FirebaseResultItem> results { get; set; }
    }
}
