using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface ILocalizationDataService : IServiceBase
    {
        Dictionary<string, LocalizablesDTO> GetDataFor<TEntity>(long Id) where TEntity : class;
        Dictionary<string, LocalizablesDTO> GetDataFor(Type t, long Id);
        SubmitResult SetDataFor<TEntity>(long id, Dictionary<string, LocalizablesDTO> dto) where TEntity : class;
        SubmitResult SetDataFor(Type type, long id, Dictionary<string, LocalizablesDTO> dto);
        SubmitResult SetDataFor(string type, long id, Dictionary<string, LocalizablesDTO> dto);

    }
}
