using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public class CollectionEFUnit<TContext> : UnitOfWork<TContext>, ICollectionUnitOfWork where TContext : DbContext
    {
        public ICollectionRepository<T> GetCollectionRepositoryFor<T>() where T : class, IModel<long>
        {
            return Store.GetInstance<CollectionRepository<T, TContext>>();
        }


    }
}
