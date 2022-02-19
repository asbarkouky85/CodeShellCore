﻿using CodeShellCore.Helpers;
using CodeShellCore.Seeding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asga.Auth.Seeding
{
    public class AuthDataSeeder : IDataSeedContributor<AuthContext>
    {
        public void SeedAsync(AuthContext context)
        {
            if (context.Users.Any())
                return;
            var domain = new Domain { Id = 1, Name = "Auth" };
            var usersResource = new Resource
            {
                Id = 1,
                Name = "Users",
                Domain = domain
            };
            var rolesResource = new Resource
            {
                Id = 2,
                Name = "Roles",
                Domain = domain
            };

            var role = new Role
            {
                Id = 1,
                Name = "Admin",
                RoleResources = new List<RoleResource> {
                    new RoleResource { Id = 1, Resource = usersResource,CanDelete=true,CanInsert=true,CanUpdate=true,CanViewDetails=true },
                    new RoleResource { Id = 2, Resource = rolesResource,CanDelete=true,CanInsert=true,CanUpdate=true,CanViewDetails=true }
                }
            };
            var u = new User
            {
                Name = "System Administrator",
                Email = "admin@codeshell.com",
                LogonName = "admin",
                Id = 1,
                Gender = true,
                Password = "827ccb0eea8a706c4c34a16891f84e7b",
                UserRoles = new List<UserRole> { new UserRole { Id = 1, Role = role } }
            };
            context.Users.Add(u);
            context.SaveChanges();
        }

        public void SeedAsync(DbContext context)
        {
            if (context is AuthContext)
                SeedAsync((AuthContext)context);
        }
    }
}