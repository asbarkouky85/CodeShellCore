using CodeShellCore.DependencyInjection;
using CodeShellCore.Files.Uploads;
using CodeShellCore.FileServer.Consumers;
using CodeShellCore.FileServer.Paths;
using CodeShellCore.MQ;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.FileServer
{
    public static class CodeShellFileServerModule
    {
        public static void AddFileServerApplication(this IServiceCollection coll)
        {
            coll.AddTransient<IAttachmentFileService, AttachmentFileService>();
            coll.AddTransient<IInternalAttachmentFileService, AttachmentFileService>();
            //coll.AddTransient<IUploadedFilesHandler, AttachmentFileService>();
            coll.AddTransient<IPathProvider, PathProvider>();

            coll.AddAutoMapper(typeof(CodeshellFileServerAutoMapperProfile).Assembly);
        }

        public static void AddFileServerConsumers(this IServiceCollection coll, Action<IRabbitMqReceiveEndpointConfigurator> other = null)
        {
            coll.AddRabbitMQServiceBus(e =>
            {
                e.Consumer<TempFileConfirmedConsumer>();
                other?.Invoke(e);
            });
        }
    }
}
