using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Asga.Auth.Dto
{
    public class ChangePasswordDTO : DTO<User,long>
    {
        
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
