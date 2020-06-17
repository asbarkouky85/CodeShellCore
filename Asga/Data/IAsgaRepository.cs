using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Lookups;
using System.Collections.Generic;

namespace Asga.Data
{
    public interface IAsgaRepository<T> :ICollectionRepository<T>,INameableRepository<T> where T:class
    {

    }
}
