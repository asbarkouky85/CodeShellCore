﻿using CodeShellCore.Web.Moldster;
using Asga.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Linq;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Data.ConfiguredCollections;
using Asga.Security;
using CodeShellCore.Helpers;
using CodeShellCore.FileServer;

namespace Example.UI
{
    public class UIShell : MoldsterShell
    {
        protected override bool useLocalization => true;
        protected override string localizationAssembly => "Example";

        protected override CultureInfo defaultCulture => new CultureInfo("en");
        public UIShell(IConfiguration config) : base(config)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddAuthModule(false);
            coll.AddAsgaWeb();
            coll.AddFileServerModule(Configuration);
        }

        public override void AddMvcFeatures(IMvcBuilder mvc)
        {
            base.AddMvcFeatures(mvc);
        }

        protected override void OnReady()
        {
            base.OnReady();
            RootInjector.GetService<ICollectionConfigService>().RegisterCollection<User>("MaleUsers", u =>
            {
                var rols = u.UserAs<UserDTO>().Roles.Select(e => long.Parse(e)).ToList();
                return d => d.UserRoles.Any(e => rols.Any(f => e.RoleId == f));
            });
            var tok = Utils.CreateClientToken(new AppClient { ClientId = "Test.App" });
            Console.WriteLine(tok);
        }
    }
}
