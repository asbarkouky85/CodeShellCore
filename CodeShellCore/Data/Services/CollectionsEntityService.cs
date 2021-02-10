using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public class CollectionsEntityService<T> : EntityService<T> where T : class, IModel<long>
    {
        protected readonly ICollectionUnitOfWork CollectionUnit;
        protected ICollectionRepository<T> CollectionRepository { get { return CollectionUnit.GetCollectionRepositoryFor<T>(); } }
        public CollectionsEntityService(ICollectionUnitOfWork unit) : base(unit)
        {
            CollectionUnit = unit;
        }

        public LoadResult LoadCollection(string collectionId, LoadOptions opts)
        {
            if (collectionId == "")
                return CollectionRepository.Find(opts.GetOptionsFor<T>());
            return CollectionRepository.LoadCollection(collectionId, opts.GetOptionsFor<T>());
        }
    }
}
