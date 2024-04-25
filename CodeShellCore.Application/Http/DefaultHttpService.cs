using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Http
{
    public class DefaultHttpService : HttpService
    {
        string _baseUrl;
        public DefaultHttpService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        protected override string BaseUrl => _baseUrl;
    }
}
