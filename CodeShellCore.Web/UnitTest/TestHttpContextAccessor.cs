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
        public TestHttpContextAccessor(IServiceProvider prov)
        {
            _context = new DefaultHttpContext();
            _context.RequestServices = prov;
            //cont?.Invoke(context);
        }

        public HttpContext HttpContext { get { return _context; } set { _context = value; } }

    }
}
