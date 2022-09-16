using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
    public interface ICustomTextService
    {
        LoadResult<CustomTextDto> Get(CustomTextRequest req, LoadOptions opts);
        SubmitResult SaveChanges(IEnumerable<CustomTextDto> lst);
    }
}
