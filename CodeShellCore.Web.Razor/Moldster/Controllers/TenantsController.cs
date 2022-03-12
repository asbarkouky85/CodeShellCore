using CodeShellCore.Moldster;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using CodeShellCore.Moldster.Tenants.Dtos;
using CodeShellCore.Moldster.Tenants.Services;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class TenantsController : EntityController<Tenant, long>, IEntityController<Tenant, long>
    {
        TenantsService _service;


        public TenantsController(TenantsService configTenantService) : base(configTenantService)
        {
            _service = configTenantService;
        }

        public override IActionResult GetSingle([FromRoute] long id)
        {
            TenantEditDTO edit = _service.GetSingleDTO(id);
            return Respond(edit);
        }

        public IActionResult Post([FromBody] Tenant obj)
        {
            return DefaultPost(obj);
        }

        public IActionResult Put([FromBody] Tenant obj)
        {
            return DefaultPut(obj);
        }
    }

}
