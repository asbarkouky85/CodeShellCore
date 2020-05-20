using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public class CollectionEFUnit<TContext> : UnitOfWork<TContext>, ICollectionUnitOfWork where TContext : DbContext
    {
        public CollectionEFUnit(IServiceProvider provider) : base(provider)
        {
        }

        public ICollectionRepository<T> GetCollectionRepositoryFor<T>() where T : class
        {
            return Store.GetInstance<ICollectionEFRepository<T,TContext>>();
        }


    }
}
