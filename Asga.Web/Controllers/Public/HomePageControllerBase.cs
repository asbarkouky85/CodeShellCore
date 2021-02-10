using Asga.Public.Data;
using Asga.Public.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Web.Controllers.Public
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class HomePageControllerBase : BaseApiController
    {
        private readonly IPublicDataService service;

        public HomePageControllerBase(IPublicDataService service)
        {
            this.service = service;
        }

        public IActionResult Get()
        {
            return Respond(service.Get());
        }
    }
}
