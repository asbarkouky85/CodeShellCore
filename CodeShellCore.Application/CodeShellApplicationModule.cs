using CodeShellCore.Data;
using CodeShellCore.Data.Events;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;
using CodeShellCore.Http;
using CodeShellCore.Localization;
using CodeShellCore.Modularity;
using CodeShellCore.MultiTenant;
using CodeShellCore.Proxy;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CodeShellCore
{
    [DependsOn(typeof(CodeShellCoreModule),
        typeof(CodeShellDomainModule),
        typeof(CodeShellApplicationContractsModule)
        )]
    public class CodeShellApplicationModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {

            context.Services.AddSingleton<IFileHandler, FileSystemHandler>();
            context.Services.AddSingleton<IClientProvider, DefaultClientProvider>();

            context.Services.AddScoped<CurrentTenant>();

            context.Services.AddTransient<IAuthenticationService, DbAuthenticationWithPermissionService>();
            context.Services.AddTransient<IUserDataService, DbUserDataService>();

            context.Services.AddTransient<IBlobContainerFactory, DefaultBlobContainerFactory>();
            context.Services.AddTransient<ICrudEventSender, CrudEventSender>();
            context.Services.AddTransient<IEmailService, EmailService>();
            context.Services.AddTransient<IHttpService, DefaultHttpService>();
            context.Services.AddTransient<ILookupsAppService, LookupsAppService>();

            context.Services.AddTransient<IUnitOfWork, DefaultUnitOfWork>();
            context.Services.AddTransient<IUploadedFilesHandler, UploadedFileHandler>();
            context.Services.AddTransient<ILocalizationDictionariesService, LocalizationDictionariesService>();

            context.Services.AddOptions<CrudEventSenderOptions>();
            context.Services.AddOptions<FileUploadOptions>("Uploads");

            context.Services.Configure<FileUploadOptions>(e =>
            {
                if (e.Default == null)
                {
                    e.Default = new BlobContainerConfiguration
                    {
                        Name = "Default",
                        RootFolder = "c:/_attachments/_fms"
                    };
                }

                if (e.Containers == null)
                {
                    e.Containers = new List<BlobContainerConfiguration>();
                }

            });

            context.Services.AddCodeShellAutoMapper();
        }
    }
}
