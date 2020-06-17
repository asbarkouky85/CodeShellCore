using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Configurator.Dtos;

namespace CodeShellCore.Moldster.Services
{
    public interface IDbTemplateProcessingService : ITemplateProcessingService
    {
        bool CollectTemplateData(long id);
        void ProcessForTenant(string templatePath, string modCode);
        bool ProcessForTenant(long id, long tenantId);
        void UpdateTemplatePages(long id, long tenantId);
        void MoveHtmlTemplate(MovePageRequest r);
        void DeleteHtmlTemplate(string tenantCode, string fromPath);
    }
}
