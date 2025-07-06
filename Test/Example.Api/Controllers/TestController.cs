using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Api.Controllers
{
    
    public class TestController : BaseApiController
    {
        [HttpGet]
       // [Route("")]
        public string Index()
        {
            return "hi";
        }
        [HttpGet]
      //  [Route("get-name")]
        public string GetName()
        {
            return "name";
        }
        [HttpGet]
        //[Route("user-name")]
        public string GetUserName()
        {
            return "user-name";
        }
    }
}
