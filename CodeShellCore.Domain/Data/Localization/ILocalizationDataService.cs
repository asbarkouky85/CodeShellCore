using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Localization
{
    public interface ILocalizationDataService : IServiceBase
    {
        Dictionary<string, LocalizablesData> GetDataFor<TEntity>(object Id) where TEntity : class;
        Dictionary<string, LocalizablesData> GetDataFor(Type t, object Id);
        SubmitResult SetDataFor<TEntity>(object id, Dictionary<string, LocalizablesData> dto) where TEntity : class;
        SubmitResult SetDataFor(Type type, object id, Dictionary<string, LocalizablesData> dto);
        SubmitResult SetDataFor(string type, object id, Dictionary<string, LocalizablesData> dto);

    }
}
