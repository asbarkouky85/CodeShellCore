using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ.Events
{
    public class SimpleEvent2
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string Type => GetType().Name;

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
