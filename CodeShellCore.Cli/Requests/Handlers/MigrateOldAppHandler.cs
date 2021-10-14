﻿using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Requests.Handlers
{
    public class MigrateOldAppHandler : CliRequestHandler<MigrateOldAppRequest>
    {
        public MigrateOldAppHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<MigrateOldAppRequest> builder)
        {
            builder.FillProperty(e => e.TenantCode, 't', "tenant", true);
            builder.FillProperty(e => e.ConfigurationApiPath, 'p', "project", true);
            builder.FillProperty(e => e.Environment, 'e', "environment", false);
        }

        protected override Task<Result> HandleAsync(MigrateOldAppRequest request)
        {
            CliDispatchShell.SetSettingsPath(request.ConfigurationApiPath);
            CliShell.ConfigurationApiPath = request.ConfigurationApiPath;
            var s = GetService<IMigrationService>();
            s.MigrateBaseModule(request.TenantCode);
            return Task.FromResult(new Result());
        }
    }
}
