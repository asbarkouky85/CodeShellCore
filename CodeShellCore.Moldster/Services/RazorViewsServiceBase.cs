using CodeShellCore.Cli;
using CodeShellCore.Http;
using CodeShellCore.Text;
using CodeShellCore.Types;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public abstract class RazorViewsServiceBase : ConsoleService
    {
        protected InstanceStore<object> Store;

        protected MoldsterModuleOptions opts;
        protected RazorViewsServiceBase(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt,
            IOutputWriter wtt) : base(wtt)
        {
            opts = opt.Value;
            Store = new InstanceStore<object>(prov);
        }

        protected void Handle(CodeShellHttpException ex)
        {
            if (ex.HttpResult != null && ex.HttpResult.ExceptionMessage.TryRead(out HttpResult res))
            {
                using (Out.Set(ConsoleColor.Red))
                {
                    Out.WriteLine();
                    Out.WriteLine("Unable to render page : " + res.Message);
                }
                using (Out.Set(ConsoleColor.DarkRed))
                {
                    Out.WriteLine(res.ExceptionMessage);
                    Out.WriteLine();
                }
            }
            else
            {

                WriteException(ex);
            }

        }
    }
}
