using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionUnitOfWork : IUnitOfWork
    {
        ICollectionRepository<T> GetCollectionRepositoryFor<T>() where T : class;
    }
}
