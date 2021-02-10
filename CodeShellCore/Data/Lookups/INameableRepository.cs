using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public interface INameableRepository<T> : IRepository<T> where T : class
    {
        IEnumerable<Named<long>> FindAsLookup(string collectionId = null);
    }
}
