using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionRepository<T> : IRepository<T> where T : class
    {
        IEnumerable<T> GetCollectionList(string collectionId);
        IEnumerable<TObject> GetCollectionListAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp);
        LoadResult<T> LoadCollection(string collectionId, ListOptions<T> opts);
        LoadResult<TObject> LoadCollectionAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp, ListOptions<TObject> opts) where TObject : class;
    }
}
