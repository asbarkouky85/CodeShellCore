using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services.Db
{
    public interface ITemplateDataService : IServiceBase
    {
        SubmitResult UpdateParameters(PageCategory p, List<PageCategoryParameterDTO> parameters);
    }
}
