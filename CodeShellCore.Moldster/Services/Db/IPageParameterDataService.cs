using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services.Db
{
    public interface IPageParameterDataService : IServiceBase
    {
        SubmitResult UpdateTemplatePages(long id, long tenantId);
        SubmitResult UpdateTemplatePagesViewParamsJson(long tenantId,long? categoryId=null);
    }
}
