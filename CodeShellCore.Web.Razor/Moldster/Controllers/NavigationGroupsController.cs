using CodeShellCore.Linq;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class NavigationGroupsController : EntityController<NavigationGroup, long>
    {
        NavigationGroupService _service;
        public NavigationGroupsController(NavigationGroupService service) : base(service)
        {
            _service = service;
        }

        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            return Respond(_service.GetAll(opt));
        }

        public IActionResult GetPageToAdd([FromQuery] LoadOptions opt)
        {
            var x = _service.GetPageToAdd(opt);
            return Respond(x);
        }
        public IActionResult GetPagesByNav([FromQuery] LoadOptions opts, [FromQuery] long naveId)
        {
            var ps = _service.GetPagesByNav(naveId, opts);
            return Respond(ps);
        }

        public IActionResult GetTenant()
        {
            return Respond(_service.GetTenant());
        }

        public IActionResult Create([FromBody] List<NavigationPage> NavigationPageListDTO)
        {
            return Respond(_service.Create(NavigationPageListDTO));
        }

        public IActionResult SetApplyOrder([FromBody] ApplyOrderDTO dto)
        {
            return Respond(_service.SetApplyOrder(dto));
        }

        public IActionResult UpdateNave([FromBody] NavigationGroup navigationGroup)
        {
            return Respond(_service.Update(navigationGroup));
        }


        public IActionResult CreateNave([FromBody] NavigationGroup navigationGroup)
        {
            return Respond(_service.CreateNave(navigationGroup));
        }

        public IActionResult DeleteNavPage(long id)
        {
            SubmitResult = _service.DeleteNavPage(id);
            return Respond();
        }
    }
}
