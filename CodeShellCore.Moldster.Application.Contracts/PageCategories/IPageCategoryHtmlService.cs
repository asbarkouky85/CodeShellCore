using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryHtmlService
    {
        Task<bool> CollectTemplateData(long id);
        Task<bool> ProcessForTenant(long id, long tenantId);
        Task ProcessForTenant(string templatePath, string modCode);
        Task UpdateTemplatePages(long id, long tenantId);
    }
}
