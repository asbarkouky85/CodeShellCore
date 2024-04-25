using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Data.Services;
using CodeShellCore.Data;
using CodeShellCore.Files.Uploads;
using CodeShellCore.Files;
using CodeShellCore.Http;
using CodeShellCore.Modularity;
using CodeShellCore.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using CodeShellCore.Data.Events;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;

namespace CodeShellCore
{
    public static class CodeShellApplicationModule
    {

        public static void AddServiceFor<T, TService>(this IServiceCollection coll) where T : class where TService : class, IEntityService<T>
        {
            coll.AddTransient<TService>();
            coll.AddTransient<IEntityService<T>, TService>();
        }
        public static void AddCodeShellAutoMapper(this IServiceCollection collection)
        {

            collection.AddAutoMapper(typeof(CodeShellAutoMapperProfile).Assembly);
            collection.AddTransient<Data.Mapping.IObjectMapper, AutoMapperObjectMapper>();
            collection.AddTransient<IQueryProjector, AutoMapperObjectMapper>();
        }

        public static void AddCodeShellApplication(this IServiceCollection coll)
        {
            coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
            coll.AddTransient<IUnitOfWork, DefaultUnitOfWork>();
            coll.AddTransient<IHttpService, DefaultHttpService>();
            coll.AddTransient<IEmailService, EmailService>();
            coll.AddTransient<IUploadedFilesHandler, UploadedFileHandler>();
            coll.AddTransient<IBlobContainerFactory, DefaultBlobContainerFactory>();
            coll.AddTransient<ICrudEventSender, CrudEventSender>();
            coll.AddSingleton<IFileHandler, FileSystemHandler>();
            coll.AddSingleton<IClientProvider, DefaultClientProvider>();
            coll.AddScoped<CurrentTenant>();
            coll.AddTransient<ITenantDataProvider, NullTenantDataProvider>();

            coll.AddOptions<CrudEventSenderOptions>();
            coll.AddOptions<FileUploadOptions>("Uploads");
            coll.Configure<FileUploadOptions>(e =>
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

            coll.AddCodeShellAutoMapper();
        }

        public static void ConfigureUploads(this IServiceCollection coll, IConfiguration conf)
        {
            coll.Configure<FileUploadOptions>(conf.GetSection("Uploads"));

        }

        public static void AddServiceFor<T, TService, ITService>(this IServiceCollection coll)
            where T : class
            where TService : class, ITService
            where ITService : class, IEntityService<T>
        {
            coll.AddTransient<TService>();
            coll.AddTransient<IEntityService<T>, TService>();
            coll.AddTransient<ITService, TService>();
        }

        public static void AddLookupsService<T>(this IServiceCollection coll) where T : class, ILookupsService
        {
            coll.AddTransient<ILookupsService, T>();
        }

        public static void AddLookupsService<T, IT>(this IServiceCollection coll)
            where IT : class, ILookupsService
            where T : class, IT
        {
            coll.AddTransient<ILookupsService, T>();
            coll.AddTransient<IT, T>();
        }
    }
}
