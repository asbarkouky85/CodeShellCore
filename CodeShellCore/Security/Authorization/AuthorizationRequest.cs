using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public class AuthorizationRequest
    {
        public string Resource { get; set; }
        public string Action { get; set; }

    }
    public class AuthorizationRequest<T> : AuthorizationRequest where T : class
    {

        public T Context { get; private set; }

        public AuthorizationRequest(T context=null)
        {
            Context = context;

        }
    }
}
