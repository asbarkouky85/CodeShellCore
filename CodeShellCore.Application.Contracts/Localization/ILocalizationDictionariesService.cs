using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Localization
{
    public interface ILocalizationDictionariesService
    {
        Task<LocalizationDictionaryDto> Get(LocalizationDictionariesRequestDto dto);
        Task SyncLanguages(string folder, string src, string target);
    }
}
