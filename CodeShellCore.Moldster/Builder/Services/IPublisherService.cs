using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Tenants.Dtos;
using CodeShellCore.Net;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Builder.Services
{
    public interface IPublisherService : IServiceBase
    {
        IOutputWriter OutputWriter { get; set; }
        string CompressSubModuleScripts(string tenant, string version);
        PublisherResult UploadTenantBundle(string tenant, string version);
        Result DeleteOtherBundlesForTenant(string tenant);
        Dictionary<string, TenantInfoItem> GetAllTenantsInfo();
        Result SetTenantInfo(string tenant, string version = null);
        Result SetAllTenantsInfo(Dictionary<string, TenantInfoItem> dic);


    }
}
