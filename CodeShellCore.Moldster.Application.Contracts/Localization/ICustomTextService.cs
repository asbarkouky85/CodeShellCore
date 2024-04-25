using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
    public interface ICustomTextService
    {
        PagedResult<CustomTextDto> Get(CustomTextRequestDto req, PagedListRequestDto opts);
        SubmitResult SaveChanges(IEnumerable<CustomTextDto> lst);
    }
}
