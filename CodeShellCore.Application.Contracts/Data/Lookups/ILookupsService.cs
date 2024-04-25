using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;

namespace CodeShellCore.Data.Lookups
{
    public interface ILookupsService
    {
        Dictionary<string, IEnumerable<NamedDto<object>>> GetRequestedLookups(Dictionary<string, string> requested);
        IEnumerable<NamedDto<object>> GetListNamed(string entityName, string collection = null);
        IEnumerable<NamedDto<object>> GetLookupNamed(Type t, string identifier);
        IEnumerable<NamedDto<object>> GetLookupNamed<TEntity>(string identifier) where TEntity : class;
        IEnumerable<NamedDto<object>> GetLookupNamed<TEntity>(string identifier, Expression<Func<TEntity, bool>> ex) where TEntity : class;
        IEnumerable<TObject> GetLookup<TObject>(string identifier) where TObject : class;
        IEnumerable<TResult> GetLookupAs<TObject, TResult>(string identifier, Expression<Func<TObject, TResult>> ex) where TObject : class where TResult : class;
        PagedResult<NamedDto<object>> GetListNamedPaged(string entityName, PagedListRequestDto dto, string identifier = null);
    }
}