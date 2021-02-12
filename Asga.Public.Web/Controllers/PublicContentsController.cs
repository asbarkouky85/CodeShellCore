using Asga.Public.Business;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Asga.Public.Web.Controllers
{
    public class PublicContentsController : MoldsterEntityController<PublicContent, long>, IEntityController<PublicContent, long>
    {
        protected readonly IPublicContentService Service;

        public PublicContentsController(IPublicContentService service) : base(service)
        {
            this.Service = service;
        }

        public virtual IActionResult GetByCode(string code, string lang)
        {
            var dto = Service.GetByCode(code, lang);
            return Respond(dto);
        }

        public virtual IActionResult GetContent(string id)
        {
            var content = Service.GetContentPage(id);
            if (ClientData.IsMobile)
            {
                return Respond(new { Content = content });
            }
            else
            {
                return Content(content, "text/html", Encoding.UTF8);
            }
        }

        public virtual IActionResult Post([FromBody] PublicContent obj)
        {
            return DefaultPost(obj);
        }

        public virtual IActionResult Put([FromBody] PublicContent obj)
        {
            return DefaultPut(obj);
        }
    }
}
