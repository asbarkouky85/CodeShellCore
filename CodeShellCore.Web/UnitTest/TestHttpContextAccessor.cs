using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.UnitTest
{

    public class TestHttpContextAccessor : IHttpContextAccessor
    {
        private HttpContext _context;
        public TestHttpContextAccessor()
        {
            _context = new DefaultHttpContext();
            
            //cont?.Invoke(context);
        }

        public HttpContext HttpContext { get { return _context; } set { _context = value; } }

    }
}
