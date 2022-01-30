using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeShellCore.Data.Lookups;

namespace CodeShellCore.Data.Services
{
    public interface ILookupsService
    {
        IEnumerable<TObject> GetLookup<TObject>(string identifier) where TObject : class;
        IEnumerable<TResult> GetLookupAs<TObject, TResult>(string identifier, Expression<Func<TObject, TResult>> ex)
            where TObject : class
            where TResult : class;
        IEnumerable<Named<object>> GetLookupNamed(Type t, string identifier);
        IEnumerable<Named<object>> GetLookupNamed<TObject>(string identifier) where TObject : class;
        IEnumerable<Named<object>> GetLookupNamed<TObject>(string identifier, Expression<Func<TObject, bool>> ex) where TObject : class;
        Dictionary<string, IEnumerable<Named<object>>> GetRequestedLookups(Dictionary<string, string> requested);
    }
}