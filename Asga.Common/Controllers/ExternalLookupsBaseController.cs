using CodeShellCore.Web.Controllers;
using Asga.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Controllers
{
    public abstract class ExternalLookupsBaseController : BaseApiController
    {
        [HttpPost]
        public IActionResult Get([FromQuery]ExternalLookupQuery query,[FromBody]Dictionary<string,string> dic)
        {
            query.Required = dic;
            return Respond(GetData(query));
        }

        protected abstract object GetData(ExternalLookupQuery q);
    }
}
