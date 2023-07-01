using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Http.Pushing
{
    public class FirebaseRequest
    {
        
        public string priority { get; set; }
        public FirebaseMessage notification { get; set; }
        public object data { get; set; }
        public string condition { get; set; }
        public string to { get; set; }
    }
}
