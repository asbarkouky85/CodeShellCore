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
        PagedResult<T> LoadCollection(string collectionId, PagedListRequest<T> opts);
        PagedResult<TObject> LoadCollectionAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp, PagedListRequest<TObject> opts) where TObject : class;
        PagedResult<TObject> LoadCollectionAndMap<TObject>(string collectionId, PagedListRequest<TObject> opts) where TObject : class;
    }
}
