using CodeShellCore.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Modularity
{
    public class CodeShellModule
    {
        public virtual void RegisterServices(CodeshellAppContext context) { }
        public virtual void Configure(CodeShellApplicationInitializationContext context) { }
        public virtual void OnApplicationStarted(CodeShellApplicationInitializationContext context) { }
        public virtual void OnApplicationStopped() { }
    }
}
