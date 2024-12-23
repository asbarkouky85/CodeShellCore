using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionRepository<T> : IRepository<T>, ICollectionRepository
        where T : class
    {

        Task<IEnumerable<T>> GetCollectionList(string collectionId);
        Task<IEnumerable<TObject>> GetCollectionListAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp);
        Task<IEnumerable<TObject>> GetCollectionListAndMap<TObject>(string collectionId);
        Task<PagedResult<T>> LoadCollection(string collectionId, PagedListRequest<T> opts);
        Task<PagedResult<TObject>> LoadCollectionAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp, PagedListRequest<TObject> opts) where TObject : class;
        Task<PagedResult<TObject>> LoadCollectionAndMap<TObject>(string collectionId, PagedListRequest<TObject> opts) where TObject : class;
    }
}
