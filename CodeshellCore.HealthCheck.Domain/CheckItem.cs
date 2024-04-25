using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.HealthCheck
{
    public class CheckItem : ChangeColumnsEntity<long>
    {
        public CheckItem() { }
        public CheckItem(string name, string host, double totalMilliseconds) : this()
        {
            ServiceName = name;
            Host = host;
            ResponseTime = totalMilliseconds;
        }

        public string Host { get; set; }
        public string ServiceName { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public double ResponseTime { get; set; }
        public string Response { get; set; }

        public void SetFailed(int statusCode, string response)
        {
            Success = false;
            Response = response;
            StatusCode = statusCode;
        }

        public void SetSuccess(int statusCode, string response)
        {
            Success = true;
            Response = response;
            StatusCode = statusCode;
        }
    }
}
