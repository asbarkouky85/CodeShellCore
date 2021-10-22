using CodeShellCore.Linq;
using CodeShellCore.Moldster.Localization.Dtos;
using CodeShellCore.Services;
using CodeShellCore.Text.ResourceReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
    public interface ILocalizationService : IServiceBase
    {
        void Import(string type, string lang, List<DataItem> strs, bool suspendOut = false);
        void GenerateJsonFiles(string moduleCode);
        void SyncLanguages(string lang1, string lang2);
        void SyncAllLanguages();
        void FixPages(string tenantCode);
        void InitializeResxFiles();
        void AddLocalizationFiles();
        void UpdateFiles(LocalizationDataCollector localization);

        LoadResult<CustomText> LoadForTenant(CustomTextRequest req, LoadOptions opts);
    }
}
