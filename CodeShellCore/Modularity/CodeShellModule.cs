using CodeShellCore.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Modularity
{
    public class CodeShellModule
    {
        public virtual void RegisterServices(CodeshellAppContext context) { }
        public virtual void OnApplicationStarted(IServiceProvider provider) { }
        public virtual void RegisterJobs(params ITimedJob[] jobs) { }
        public virtual void Configure(CodeshellAppContext context) { }
    }
}
