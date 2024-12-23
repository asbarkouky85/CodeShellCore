using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPagesDataService
    {
        Task<IEnumerable<long>> GetPagesWithJsonParams(string modCode);
        Task<SubmitResult> ViewParamsToData(long id);
    }
}
