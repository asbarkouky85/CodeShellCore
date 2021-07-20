﻿using CodeShellCore.Cli.Requests;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Builder;
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
        }

        protected override Task<Result> HandleAsync(MigrateOldAppRequest request)
        {
            var s = GetService<IMigrationService>();
            s.MigrateBaseModule(request.TenantCode);
            return Task.FromResult(new Result());
        }
    }
}
