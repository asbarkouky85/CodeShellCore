using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services.Db
{
    public interface IPagesDataService
    {
        IEnumerable<long> GetPagesWithJsonParams(string modCode);
        SubmitResult ViewParamsToData(long id);
    }
}
