﻿using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageParameterDataService : IServiceBase
    {
        SubmitResult UpdateTemplatePages(long id, long tenantId);
        SubmitResult UpdateTemplatePagesViewParamsJson(long tenantId, long? categoryId = null);
        LoadResult<PageReferenceDTO> GetReferences(ParameterRequest req, LoadOptions opt);
    }
}
