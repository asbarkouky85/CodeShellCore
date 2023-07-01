using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Routing
{
    public interface ICliRequestHandler
    {
        Task<Result> HandleAsync(string[] args);
        string FunctionDescription { get; }
        void Document();
    }


}
