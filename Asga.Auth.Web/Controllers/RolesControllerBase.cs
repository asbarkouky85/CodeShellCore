using Asga.Auth;
using Asga.Auth.Services;
using CodeShellCore.Data.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Web.Controllers
{
    public class RolesControllerBase : MoldsterEntityController<Role, long>
    {
        private readonly IRolesService service;
        private readonly IAuthLookupService lookups;

        public RolesControllerBase(IRolesService service, IAuthLookupService lookups) : base(service)
        {
            this.service = service;
            this.lookups = lookups;
        }

        public virtual IActionResult Post([FromBody] Role obj)
        {
            return DefaultPost(obj);
        }

        public virtual IActionResult Put([FromBody] Role obj)
        {
            return DefaultPut(obj);
        }

        public override IActionResult GetSingle([FromRoute] long id)
        {
            return Respond(service.GetSingleEditingDTO(id));
        }

        public override IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(lookups.GetRequestedLookups(data));
        }

        public override IActionResult GetListLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(new { });
        }
    }
}
