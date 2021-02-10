using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Services.Email
{
    public class EmailRequest
    {
        public IEnumerable<EmailRecepient> Recepients { get; set; }
    }
}
