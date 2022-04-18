using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionRepository<T> : IRepository<T>, ICollectionRepository
        where T : class
    {

        IEnumerable<T> GetCollectionList(string collectionId);
        IEnumerable<TObject> GetCollectionListAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp);
        IEnumerable<TObject> GetCollectionListAndMap<TObject>(string collectionId);
        LoadResult<T> LoadCollection(string collectionId, ListOptions<T> opts);
        LoadResult<TObject> LoadCollectionAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp, ListOptions<TObject> opts) where TObject : class;
        LoadResult<TObject> LoadCollectionAndMap<TObject>(string collectionId, ListOptions<TObject> opts) where TObject : class;
    }
}
