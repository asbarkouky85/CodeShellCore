using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories.Services
{
    public interface IPageCategoryHtmlService
    {
        bool CollectTemplateData(long id);
        void ProcessForTenant(string templatePath, string modCode);
        bool ProcessForTenant(long id, long tenantId);
        void UpdateTemplatePages(long id, long tenantId);
    }
}
