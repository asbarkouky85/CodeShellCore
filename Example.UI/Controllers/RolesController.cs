using Asga.Auth;
using Asga.Auth.Services;
using Asga.Auth.Web.Controllers;
using CodeShellCore.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Example.UI.Controllers
{

    public class RolesController : RolesControllerBase
    {
        public RolesController(IRolesService service, IAuthLookupService lookups) : base(service, lookups)
        {
        }

        
        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            return base.Get(opt);
        }

        
        public override IActionResult GetSingle([FromRoute] long id)
        {
            return base.GetSingle(id);
        }

        
        public override IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return base.GetEditLookups(data);
        }

        
        public override IActionResult Post([FromBody] Role obj)
        {
            return base.Post(obj);
        }

        
        public override IActionResult Put([FromBody] Role obj)
        {
            return base.Put(obj);
        }
    }
}
