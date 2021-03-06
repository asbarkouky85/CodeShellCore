﻿using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data
{
    public interface IPageParameterDataService : IServiceBase
    {
        SubmitResult UpdateTemplatePages(long id, long tenantId);
        SubmitResult UpdateTemplatePagesViewParamsJson(long tenantId,long? categoryId=null);
        LoadResult<PageReferenceDTO> GetReferences(ParameterRequestDTO req, LoadOptions opt);
    }
}
