using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface ILocalizationDataService : IServiceBase
    {
        Dictionary<string, LocalizablesDTO> GetDataFor<TEntity>(object Id) where TEntity : class;
        Dictionary<string, LocalizablesDTO> GetDataFor(Type t, object Id);
        SubmitResult SetDataFor<TEntity>(object id, Dictionary<string, LocalizablesDTO> dto) where TEntity : class;
        SubmitResult SetDataFor(Type type, object id, Dictionary<string, LocalizablesDTO> dto);
        SubmitResult SetDataFor(string type, object id, Dictionary<string, LocalizablesDTO> dto);

    }
}
