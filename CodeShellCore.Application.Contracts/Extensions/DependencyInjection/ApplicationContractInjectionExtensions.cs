using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Extensions.DependencyInjection
{
    public static class ApplicationContractInjectionExtensions
    {

        public static void AddJobRunner<T>(this IServiceCollection coll) where T : class, IJobRunner
        {
            coll.AddTransient<IJobRunner, T>();
        }
    }
}
