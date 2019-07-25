using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Lookups;
using System.Collections.Generic;

namespace Asga.Data
{
    public interface IAsgaRepository<T> : ICollectionRepository<T> where T : class
    {
        IEnumerable<Named<long>> FindAsLookup(string collectionId = null);
    }
}
