using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Net;
using CodeShellCore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder
{
    public interface IPublisherService : IServiceBase
    {
        IOutputWriter OutputWriter { get; set; }

        Task<PublisherResult> UploadTenantBundle(string tenant, string version);
        Task<Result> DeleteOtherBundlesForTenant(string tenant);
        Task<Dictionary<string, TenantInfoItem>> GetAllTenantsInfo();
        Task<Result> SetTenantInfo(string tenant, string version = null);
        Task<Result> SetAllTenantsInfo(Dictionary<string, TenantInfoItem> dic);


    }
}
