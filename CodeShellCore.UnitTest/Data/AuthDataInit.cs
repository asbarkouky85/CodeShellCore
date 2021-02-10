using Asga.Auth;
using System.Collections.Generic;
using CodeShellCore.Text;
using System;

namespace CodeShellCore.UnitTest.Data
{
    public class AuthDataInit
    {
        private readonly AuthContext context;

        public AuthDataInit(AuthContext context)
        {
            this.context = context;
        }

        public void InitializeForRoles()
        {
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
            context.SaveChanges();
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
                },
                UserEntityLinks = new[] {
                    new UserEntityLink{ Id=1,EntityId=3,EntityName="Customer",UserId=1},
                    new UserEntityLink{ Id=2,EntityId=4,EntityName="Customer",UserId=1},
                    new UserEntityLink{ Id=3,EntityId=10,EntityName="Warehouse",UserId=1},
                }
            });
            context.SaveChanges();
        }

    }
}
