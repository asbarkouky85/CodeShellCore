using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;

namespace CodeShellCore.Moldster.Configurator.Services
{
    public class TenantsService : EntityService<Tenant>, ITenantService
    {
        private readonly IConfigUnit unit;

        public TenantsService(IConfigUnit unit) : base(unit)
        {
            this.unit = unit;
        }

        public override SubmitResult Create(Tenant obj)
        {
            if (obj.Id == 0)
            {
                long id = unit.TenantRepository.GetMax(d => d.Id);
                obj.Id = id + 1;
            }
            if (!unit.TenantRepository.Exist(d => true))
            {
                obj.IsActive = true;
            }
            return base.Create(obj);
        }

        public TenantEditDTO GetSingleDTO(long id)
        {

            return unit.TenantRepository.FindSingleAs(d => new TenantEditDTO { Entity = d, Id = d.Id }, id);
        }
    }
}
