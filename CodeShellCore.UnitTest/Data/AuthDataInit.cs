using Asga.Auth;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Text;

namespace CodeShellCore.UnitTest.Data
{
    public class AuthDataInit
    {
        private readonly AuthContext context;

        public AuthDataInit(AuthContext context) {
            this.context = context;
        }

        public void IntitializeUsersData()
        {
            context.Resources.Add(new Resource
            {
                Id = 1,
                Name = "Users"
            });

            context.Resources.Add(new Resource
            {
                Id = 2,
                Name = "Roles"
            });

            context.Roles.Add(new Role
            {
                Id = 1,
                Name = "Admin",
                RoleResources = new List<RoleResource>
                {
                    new RoleResource { Id=1,ResourceId=1,CanViewDetails=true,CanDelete=true },
                    new RoleResource { Id=2,ResourceId=2,CanViewDetails=true,CanDelete=false }
                }
            });
            context.Users.Add(new User
            {
                Id = 1,
                LogonName = "admin",
                Password = "12345".ToMD5(),
                UserRoles = new List<UserRole> {
                    new UserRole{ Id=1,RoleId=1}
                }
            });
            context.SaveChanges();
        }
    }
}
