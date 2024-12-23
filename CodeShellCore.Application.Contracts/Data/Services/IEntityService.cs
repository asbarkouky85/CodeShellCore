using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.MQ.Events;
using CodeShellCore.Services;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Services
{
    public interface IEntityService : IServiceBase
    {
        //LoadResult LoadObjects(LoadOptions opts);
        Task<DeleteResult> DeleteById(object prime);
        Task<object> GetSingleObject(object id);
        Task<SubmitResult> Create(string obj);
        Task<SubmitResult> Update(string obj);

    }

    public interface IEntityService<T> : IEntityService where T : class
    {
        Task<T> GetSingle(object id);
        Task<SubmitResult> Create(T obj);
        Task<SubmitResult> Update(T obj);
        Task<DeleteResult> Delete(T prime);
        Task<DeleteResult> CanDelete(object Id);
        //IRepository<T> Repository { get; }

        Task<PagedResult<T>> Load(PagedListRequestDto opts);
        Task<PagedResult<TDTO>> LoadDTO<TDTO>(Expression<Func<T, TDTO>> ex, PagedListRequestDto opts) where TDTO : class;
        Task<PagedResult<T>> LoadCollection(string collectionId, PagedListRequestDto opts);
        Task<PagedResult<TDto>> LoadCollectionAs<TDto>(string collectionId, Expression<Func<T, TDto>> ex, PagedListRequestDto opts) where TDto : class;
        Task<bool> IsUnique(PropertyUniqueDTO dto);
    }


}
