using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
    public interface ICustomTextService
    {
        LoadResult<CustomTextDto> Get(CustomTextRequestDto req, LoadOptions opts);
        SubmitResult SaveChanges(IEnumerable<CustomTextDto> lst);
    }
}
