using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Localization
{
    public interface ILocalizationDataService : IServiceBase
    {
        Task<Dictionary<string, LocalizablesData>> GetDataFor<TEntity>(object Id) where TEntity : class;
        Task<Dictionary<string, LocalizablesData>> GetDataFor(Type t, object Id);
        Task<SubmitResult> SetDataFor<TEntity>(object id, Dictionary<string, LocalizablesData> dto) where TEntity : class;
        Task<SubmitResult> SetDataFor(Type type, object id, Dictionary<string, LocalizablesData> dto);
        Task<SubmitResult> SetDataFor(string type, object id, Dictionary<string, LocalizablesData> dto);

    }
}
