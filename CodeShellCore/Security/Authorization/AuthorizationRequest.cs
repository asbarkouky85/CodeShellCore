using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public abstract class AuthorizationRequest
    {
        public string Resource { get; set; }
        public string Action { get; set; }
        
    }
    public class AuthorizationRequest<T> : AuthorizationRequest
    {
       
        public T Context { get; private set; }

        public AuthorizationRequest(T context)
        {
            Context = context;

        }
    }
}
