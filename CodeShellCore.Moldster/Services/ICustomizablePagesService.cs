using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface ICustomizablePagesService : ITemplateProcessingService
    {
        void ProcessAllTemplates(string modCode, IScriptGenerationService tsGen);
        void ProcessDomainTemplates(string domain, string modCode, IScriptGenerationService tsGen);
        void ProcessTemplate(string templatePath, string modCode, IScriptGenerationService tsGen);
    }
}
