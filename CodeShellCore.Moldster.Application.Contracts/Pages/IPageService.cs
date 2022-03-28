using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageService : IDtoEntityService<long, LoadOptions, PageListDTO, CreatePageDTO, CreatePageDTO, PageDto>
    {
    }
}
