using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.MQ.Events;
using CodeShellCore.Services;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Data.Services
{
    public interface IEntityService : IServiceBase
    {
        LoadResult LoadObjects(LoadOptions opts);
        DeleteResult DeleteById(object prime);
        object GetSingleObject(object id);
        SubmitResult Create(string obj);
        SubmitResult Update(string obj);

    }

    public interface IEntityService<T> : IEntityService where T : class
    {
        T GetSingle(object id);
        SubmitResult Create(T obj);
        SubmitResult Update(T obj);
        DeleteResult Delete(T prime);
        DeleteResult CanDelete(object Id);
        IRepository<T> Repository { get; }

        LoadResult<T> Load(LoadOptions opts);
        LoadResult<TDTO> LoadDTO<TDTO>(Expression<Func<T, TDTO>> ex, LoadOptions opts) where TDTO : class;
        LoadResult<T> LoadCollection(string collectionId, LoadOptions opts);
        LoadResult<TDto> LoadCollectionAs<TDto>(string collectionId, Expression<Func<T, TDto>> ex, LoadOptions opts) where TDto : class;
        bool IsUnique(PropertyUniqueDTO dto);
    }

    
}
