using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public class AppClient
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Address { get; set; }
    }
}
