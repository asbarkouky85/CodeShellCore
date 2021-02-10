using CodeShellCore.Cli;
using CodeShellCore.Moldster.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Cli
{
    public class LocalizationConsoleController : ConsoleController
    {
        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1,"InitializeResx"},
            { 2,"SyncLanguages"}
        };

        ILocalizationService Localization { get { return GetService<ILocalizationService>(); } }

        public void InitializeResx()
        {
            Localization.InitializeResxFiles();
        }

        public void SyncLanguages()
        {

            Localization.SyncLanguages("ar", "en");
        }

    }
}
