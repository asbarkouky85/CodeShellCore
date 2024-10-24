using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.CliDispatch.Routing
{
    public interface ICliRequestHandler
    {
        Task<Result> HandleAsync(string[] args);
        string FunctionDescription { get; }
        void Document();
        void AddArgs(Dictionary<string, string> args);
    }


}
