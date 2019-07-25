using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace CodeShellCore.Security
{
    public class DefaultPrincipal : ClaimsPrincipal
    {
        private IIdentity _identity;
        public override IIdentity Identity { get { return _identity; } }

        public string Name { get; set; }
        
        public DefaultPrincipal(string name, string type = null)
        {
            Name = name;
            _identity = new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, name) }, type);

        }

        public override bool IsInRole(string role)
        {
            return true;
        }
    }
}
