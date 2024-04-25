using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionEFRepository<T, TContext> : ICollectionRepository<T> where T : class
    {
    }
}
