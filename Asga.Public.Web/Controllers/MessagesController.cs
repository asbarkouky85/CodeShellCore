using Asga.Public.Business;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Web.Controllers
{
   public class MessagesController : MoldsterEntityController<Message, long>
    {
       protected IPublicUnit PublicUnit => GetService<IPublicUnit>();
        public MessagesController(IEntityService<Message> service) : base(service)
        {
        }

        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            var d = PublicUnit.MessageRepository.FindAsMessageListDTO(opt.GetOptionsFor<MessageListDTO>());
            return Respond(d);
        }

        public override IActionResult GetCollection(string id, [FromQuery] LoadOptions opts)
        {
            var d = PublicUnit.MessageRepository.FindAsMessageListDTO(opts.GetOptionsFor<MessageListDTO>(), id);
            return Respond(d);
        }

        [ApiAuthorize(AllowAnonymous = true)]
        public virtual IActionResult Post([FromBody] Message obj)
        {
            return DefaultPost(obj);
        }


        public IActionResult Put([FromBody] Message obj)
        {
            return DefaultPut(obj);
        }
    }
}
