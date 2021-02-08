using CodeShellCore.Data.EntityFramework;
using CodeShellCore.DependencyInjection;
using CodeShellCore.FileServer.Business;
using CodeShellCore.FileServer.Business.Internal;
using CodeShellCore.FileServer.Data;
using CodeShellCore.FileServer.Paths;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public static class FileServerModule
    {
        public static void AddFileServerModule(this IServiceCollection coll, IConfiguration conf)
        {
            var conn = conf.GetConnectionString("FileServer");
            if (string.IsNullOrWhiteSpace(conn))
                conn = conf.GetConnectionString("Default");
            coll.AddDbContext<FileServerDbContext>(e => e.UseSqlServer(conn));
            coll.AddUnitOfWork<FileServerUnit, IFileServerUnit>();
            coll.AddGenericRepository(typeof(Repository_Int64<,>));

            coll.AddTransient<IAttachmentFileService, AttachmentFileService>();
            coll.AddTransient<IPathProvider, PathProvider>();
        }
    }
}
