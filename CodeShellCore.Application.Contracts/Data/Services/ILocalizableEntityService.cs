using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Localization;
using System.Collections.Generic;

namespace CodeShellCore.Data.Services
{
    public interface ILocalizableEntityService<T, TPrime>
        where T : class, IModel<TPrime>
    {
        Dictionary<string, LocalizablesDTO> GetLocalizationData(TPrime id);
        SubmitResult SetLocalizationData(TPrime id, Dictionary<string, LocalizablesDTO> data);
    }
}
