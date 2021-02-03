using Asga.Mobile.Business;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Web.Conrollers
{
    public class NotificationsController : EntityController<Notification, long>
    {
        protected readonly INotificationService Service;
        protected IAsgaMobileUnit Unit => GetService<IAsgaMobileUnit>();

        public NotificationsController(INotificationService service) : base(service)
        {
            this.Service = service;
        }

        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            
            return Respond(Service.LoadListDTO(opt.GetOptionsFor<NotificationListDTO>()));
        }

        public IActionResult GetCount()
        {
            int c = Service.GetCount(true);
            return Respond(c);
        }

        public IActionResult SetRead(long id)
        {
            SubmitResult = Service.SetRead(id);
            return Respond();
        }
    }
}
