using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Services
{
    public interface ILocalizableEntityService<TPrime>
    {
        Task<Dictionary<string, LocalizablesDto>> GetLocalizationData(TPrime id);
        Task<SubmitResult> SetLocalizationData(TPrime id, Dictionary<string, LocalizablesDto> data);
    }
}
