using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data
{
    public interface IPagesDataService
    {
        IEnumerable<long> GetPagesWithJsonParams(string modCode);
        SubmitResult ViewParamsToData(long id);
    }
}
