using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Services.Email
{
    public class EmailRecepient
    {
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public Result SendResult { get; set; }
    }
}
