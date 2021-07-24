using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Asga.Auth.Dto
{
    public class UserListDTO : CodeShellCore.Data.Helpers.BaseDTO<long>//, IDTO<User>
    {
        public string Name { get; set; }
        public string LogonName { get; set; }
        public string AppName { get; set; }
        public string Email { get; set; }
        public long? PersonId { get; set; }
        public string Mobile { get; set; }
        public string TenantName { get; set; }
        public string GenderName { get; set; }
        public DateTime? BirthDate { get; set; }
            

    }
}
