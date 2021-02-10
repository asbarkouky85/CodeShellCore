using CodeShellCore.Data.Lookups;
using CodeShellCore.Security;
using CodeShellCore.Data.Services;
using Asga.Auth.Dto;
using Asga.Auth.Data;
using System.Collections.Generic;
using System.Dynamic;

namespace Asga.Auth.Services
{
    public class AuthLookupService : LookupsService<AuthUnit>, IAuthLookupService
    {
        private readonly IUserAccessor _accessor;

        public AuthLookupService(AuthUnit unit, IUserAccessor accessor) : base(unit)
        {
            _accessor = accessor;
        }

        public object RolesEdit(Dictionary<string, string> data)
        {
            dynamic lookups = new ExpandoObject();
            if (data.ContainsKey("domains"))
            {

                lookups.domains = Unit.DomainRepository.FindAs(DomainWithResourcesDTO.Expression);
            }
            return lookups;
        }

        public object UserEdit(Dictionary<string, string> data)
        {
            dynamic lookups = new ExpandoObject();
            if (data.ContainsKey("domains"))
            {
                lookups.domains = Unit.DomainRepository.FindAs(DomainWithResourcesDTO.Expression);
            }

            if (data.ContainsKey("roles"))
                lookups.roles = Unit.RoleRepository.FindAs(d => new Named<long>
                {
                    Id = d.Id,
                    Name = d.Name
                }, e => e.IsUserRole == false);

            if (data.ContainsKey("apps"))
                lookups.apps = Unit.AppRepository.FindAs(e => new Named<long> { Id = e.Id, Name = e.Name });
            return lookups;
        }
    }
}
