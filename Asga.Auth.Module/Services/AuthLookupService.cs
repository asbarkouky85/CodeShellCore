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

        protected override string EntitiesAssembly => "Asga.Auth";
        protected override string EntitiesNameSpace => "Asga.Auth";

        protected override IEnumerable<Named<object>> GetListNamed(string name, string collection = null)
        {
            switch (name)
            {
                case "roles":
                    return GetLookupNamed<Role>(collection, d => d.IsUserRole == false);
                case "resourcesByDomain":
                    return Unit.AsgaResourceRepository.GetClassifiedByDomain(GetCollectionId(collection));
                case "resources":
                    return Unit.AsgaResourceRepository.GetResourcesWithActions(GetCollectionId(collection));
            }
            return base.GetListNamed(name, collection);
        }
        
    }
}
