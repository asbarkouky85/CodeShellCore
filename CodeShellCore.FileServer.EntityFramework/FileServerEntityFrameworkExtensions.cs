using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public static class FileServerEntityFrameworkExtensions
    {
        public static void AddFileServerEntityFramework(this IServiceCollection services, string migrationAssembly = null)
        {
            
        }
    }
}
