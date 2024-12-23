using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.HealthCheck
{
    public class CheckItem : ChangeColumnsEntity<long>
    {
        static Dictionary<string, Guid?> _eventByService = new Dictionary<string, Guid?>();
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
        public Guid? EventId { get; set; }

        public void SetFailed(int statusCode, string response)
        {
            Success = false;
            Response = response;
            StatusCode = statusCode;
            if (!_eventByService.TryGetValue(ServiceName, out Guid? id) || id == null)
            {
                _eventByService[ServiceName] = Guid.NewGuid();
            }
            EventId = _eventByService[ServiceName];
        }

        public void SetSuccess(int statusCode, string response)
        {
            Success = true;
            Response = response;
            StatusCode = statusCode;
            _eventByService[ServiceName] = null;
        }
    }
}
