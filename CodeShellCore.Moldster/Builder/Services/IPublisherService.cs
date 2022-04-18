using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Net;
using CodeShellCore.Services;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Builder.Services
{
    public interface IPublisherService : IServiceBase
    {
        IOutputWriter OutputWriter { get; set; }

        PublisherResult UploadTenantBundle(string tenant, string version);
        Result DeleteOtherBundlesForTenant(string tenant);
        Dictionary<string, TenantInfoItem> GetAllTenantsInfo();
        Result SetTenantInfo(string tenant, string version = null);
        Result SetAllTenantsInfo(Dictionary<string, TenantInfoItem> dic);


    }
}
