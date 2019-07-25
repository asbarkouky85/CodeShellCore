using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class CustomizableViewsMoldsterService : MoldsterService
    {
        ICustomizablePagesService _custom;
        IScriptGenerationService _ts;
        public CustomizableViewsMoldsterService(
            WriterService wt,
            PathProvider paths,
            IScriptGenerationService ts,
            ICustomizablePagesService html,
            ILocalizationService loc,
            IDataService data) : base(wt, paths, ts, html, loc, data)
        {
            _custom = html;
            _ts = ts;
        }

        public override void ProcessTemplates(string modCode, string domain = null)
        {
            if (domain == null)
                _custom.ProcessAllTemplates(modCode, _ts);
            else
                _custom.ProcessDomainTemplates(domain, modCode, _ts);
        }

    }
}
