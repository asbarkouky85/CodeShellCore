using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public class CollectionsEntityService<T> : EntityService<T> where T : class
    {
        protected readonly ICollectionUnitOfWork CollectionUnit;
        protected ICollectionRepository<T> CollectionRepository { get { return CollectionUnit.GetCollectionRepositoryFor<T>(); } }
        public CollectionsEntityService(ICollectionUnitOfWork unit) : base(unit)
        {
            CollectionUnit = unit;
        }

        public virtual LoadResult<T> LoadCollection(string collectionId, LoadOptions opts)
        {
            if (string.IsNullOrEmpty(collectionId))
                return CollectionRepository.Find(opts.GetOptionsFor<T>());
            return CollectionRepository.LoadCollection(collectionId, opts.GetOptionsFor<T>());
        }

        public virtual LoadResult<TDto> LoadCollectionAs<TDto>(string collectionId, Expression<Func<T, TDto>> ex, LoadOptions opts) where TDto : class
        {
            if (string.IsNullOrEmpty(collectionId))
                return CollectionRepository.FindAs(ex, opts.GetOptionsFor<TDto>());
            return CollectionRepository.LoadCollectionAs(collectionId, ex, opts.GetOptionsFor<TDto>());
        }
    }
}
