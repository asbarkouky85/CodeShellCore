using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Modularity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore
{
    [DependsOn(typeof(CodeShellDomainModule))]
    public class CodeShellEntityFrameworkModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddGenericRepository(typeof(Repository_Int64<,>));
            context.Services.AddTransient(typeof(KeyRepository<,,>));
        }
    }
}
