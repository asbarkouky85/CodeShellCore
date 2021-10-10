using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.DependecyInjection
{
    public static class ApplicationContractInjectionExtensions
    {

        public static void AddTimedJobs(this IServiceCollection coll, IEnumerable<ITimedJob> jobs)
        {
            JobConfig conf = new JobConfig(jobs);
            coll.AddTransient<IJobRunner, JobRunner>();
            coll.AddSingleton(conf);
        }

        public static void AddTimedJobs<T>(this IServiceCollection coll, IEnumerable<ITimedJob> jobs) where T : class, IJobRunner
        {
            JobConfig conf = new JobConfig(jobs);
            coll.AddTransient<IJobRunner, T>();
            coll.AddSingleton(conf);
        }
    }
}
