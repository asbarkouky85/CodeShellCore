using CodeShellCore.Linq;
using CodeShellCore.Services;
using CodeShellCore.Text.ResourceReader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Localization
{
    public interface ILocalizationService : IServiceBase
    {
        Task Import(string type, string lang, List<DataItem> strs, bool suspendOut = false);
        Task GenerateJsonFiles(string moduleCode);
        Task SyncLanguages(string lang1, string lang2);
        Task SyncAllLanguages();
        Task FixPages(string tenantCode);
        Task InitializeResxFiles();
        Task AddLocalizationFiles();
        Task UpdateFiles(LocalizationDataCollector localization);

        Task<PagedResult<CustomTextDto>> LoadForTenant(CustomTextRequestDto req, PagedListRequestDto opts);
    }
}
