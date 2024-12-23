using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;

namespace CodeShellCore.Data.Lookups
{
    public interface ILookupsService
    {
        Task<Dictionary<string, IEnumerable<Named<object>>>> GetRequestedLookups(Dictionary<string, string> requested);
        Task<IEnumerable<Named<object>>> GetListNamed(string entityName, string collection = null);
        Task<IEnumerable<Named<object>>> GetLookupNamed(Type t, string identifier);
        Task<IEnumerable<Named<object>>> GetLookupNamed<TEntity>(string identifier) where TEntity : class;
        Task<IEnumerable<Named<object>>> GetLookupNamed<TEntity>(string identifier, Expression<Func<TEntity, bool>> ex) where TEntity : class;
        Task<IEnumerable<TObject>> GetLookup<TObject>(string identifier) where TObject : class;
        Task<IEnumerable<TResult>> GetLookupAs<TObject, TResult>(string identifier, Expression<Func<TObject, TResult>> ex) where TObject : class where TResult : class;
        Task<PagedResult<Named<object>>> GetListNamedPaged(string entityName, PagedListRequestDto dto, string identifier = null);
    }
}