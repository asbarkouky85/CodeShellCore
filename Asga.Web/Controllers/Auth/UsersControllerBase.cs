using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Asga.Auth.Dto;
using Asga.Auth.Services;

using CodeShellCore.Data.Helpers;
using CodeShellCore.Web;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;

namespace Asga.Auth.Web
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class UsersControllerBase : MoldsterEntityController<User, long>
    {
        protected readonly IUsersService service;
        protected readonly IAuthLookupService lookups;

        public UsersControllerBase(IUsersService service, IAuthLookupService lookups) : base(service)
        {
            this.service = service;
            this.lookups = lookups;
        }

        [ApiAuthorize(AllowAnonymous = false)]
        public override IActionResult GetSingle([FromRoute] long id)
        {
            return Respond(service.GetSingleEditingDTO(id));
        }

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual IActionResult ChangePassword(ChangePasswordDTO dto)
        {
            SubmitResult = service.ChangePassword(dto);
            return Respond();
        }

        [ApiAuthorize(AllowAnonymous = false)]
        public virtual IActionResult Post([FromBody] User obj)
        {
            return DefaultPost(obj);
        }

        [ApiAuthorize(AllowAnonymous = false)]
        public virtual IActionResult Put([FromBody] User obj)
        {
            return DefaultPut(obj);
        }

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual IActionResult ResetPassword(ResetDTO dto)
        {
            SubmitResult = service.ResetPassword(dto);
            return Respond();
        }

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual IActionResult SendResetMail(string email)
        {
            var host = Request.GetHostUrl();
            SubmitResult = service.ResetPassword_SendMail(email, host);
            return Respond();
        }

        [ApiAuthorize(AllowAnonymous =false)]
        public virtual IActionResult GetUserRole(long id)
        {
            var s = service.GetUserRole(id);
            return Respond(s);
        }

        [ApiAuthorize(AllowAnonymous = false)]
        public override IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(lookups.UserEdit(data));
        }

        [ApiAuthorize(AllowAnonymous = false)]
        public override IActionResult GetListLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(new { });
        }
    }
}
