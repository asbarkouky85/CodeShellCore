using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.CliDispatch.Routing
{
    public interface ICliRequestHandler
    {
        Task<Result> HandleAsync(string[] args, CancellationToken token);
        string FunctionDescription { get; }
        bool RunInBackground { get; }

        void Document();
        void AddArgs(Dictionary<string, string> args);
    }


}
