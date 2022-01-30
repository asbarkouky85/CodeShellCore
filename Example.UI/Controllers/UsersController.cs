using Asga.Auth;
using Asga.Auth.Services;
using Asga.Auth.Web.Controllers;
using CodeShellCore.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Example.UI.Controllers
{

    public class UsersController : UsersControllerBase
    {
        public UsersController(IUsersService service, IAuthLookupService lookups) : base(service, lookups)
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

        
        public override IActionResult Post([FromBody] User obj)
        {
            return base.Post(obj);
        }

        
        public override IActionResult Put([FromBody] User obj)
        {
            return base.Put(obj);
        }

        
        public override IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return base.GetEditLookups(data);
        }
    }
}
