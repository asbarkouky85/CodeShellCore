using Asga.Public.Business;
using CodeShellCore.Linq;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Web.Controllers
{
   public class HomeSlidesController : MoldsterEntityController<HomeSlide, long>, IEntityController<HomeSlide, long>
    {
        protected readonly IHomeSlideService Service;

        public HomeSlidesController(IHomeSlideService service) : base(service)
        {
            this.Service = service;
        }

        [ApiAuthorize(AllowAnonymous = true)]
        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            return base.Get(opt);
        }
        public virtual IActionResult Post([FromBody] HomeSlide obj)
        {
            return DefaultPost(obj);
        }

        public virtual IActionResult Put([FromBody] HomeSlide obj)
        {
            return DefaultPut(obj);
        }

        public virtual IActionResult SetSorting([FromBody]IEnumerable<long> ids)
        {
            SubmitResult = Service.SetSorting(ids);
            return Respond();
        }

        
    }
}
