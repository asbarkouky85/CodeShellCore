using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data
{
    public interface ITemplateDataService : IServiceBase
    {
        SubmitResult UpdateParameters(PageCategory p, List<PageCategoryParameterDTO> parameters);
    }
}
