using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;

namespace CodeShellCore.Data
{
    public interface IRepository
    {
        int Count();
        IEnumerable<Named<object>> FindAsLookup(string collectionId = null);
        IEnumerable All();
        
        
    }

    
}
