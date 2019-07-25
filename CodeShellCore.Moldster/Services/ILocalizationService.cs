using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface ILocalizationService : IServiceBase
    {
        void GenerateJsonFiles(string moduleCode);
        void SyncLanguages(string lang1, string lang2);
    }
}
