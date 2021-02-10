using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface IDbViewsService : IViewsService
    {
        TemplateDataCollector GetTemplateData(long id);
    }
}
