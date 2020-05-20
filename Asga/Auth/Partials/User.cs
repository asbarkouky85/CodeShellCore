using CodeShellCore.Security.Authentication;
using CodeShellCore.Text;
using Asga.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using CodeShellCore.Files;

namespace Asga.Auth
{
    public partial class User : IRegisterModel
    {
        [NotMapped]
        public string PasswordConfim { get; set; }
        [NotMapped]
        public Role Role { get; set; }

        [IgnoreDataMember]
        [NotMapped]
        public UserTypes UserTypeEnum
        {
            get { return (UserTypes)UserType; }
            set { UserType = (int)value; }
        }
        [NotMapped]
        public string UserTypeString { get { return ((UserTypes)UserType).GetString(); } }
        [NotMapped]
        public IEnumerable<UserRole> Roles { get; set; }
        [NotMapped]
        public long? RoleId { get; set; }

        [NotMapped]
        public TmpFileData PhotoFile { get; set; }
        
    }
}
