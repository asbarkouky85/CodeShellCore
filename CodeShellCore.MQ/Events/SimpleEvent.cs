using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ.Events
{
    public class SimpleEvent : INotification
    {
        public long Id { get; set; }
        public string Message { get; set; }
    }
}
