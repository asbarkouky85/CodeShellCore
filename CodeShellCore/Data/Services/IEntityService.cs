using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.MQ.Events;
using CodeShellCore.Services;

namespace CodeShellCore.Data.Services
{
    public interface IEntityService: IServiceBase
    {
        LoadResult LoadObjects(LoadOptions opts);
        DeleteResult DeleteById(object prime);
        object GetSingleObject(object id);
        SubmitResult Create(string obj);
        SubmitResult Update(string obj);
        
    }

    public interface IEntityService<T> : IEntityService where T : class
    {
        LoadResult<T> Load(LoadOptions opts);
        T GetSingle(object id);
        SubmitResult Create(T obj);
        SubmitResult Update(T obj);
        DeleteResult Delete(T prime);
        DeleteResult CanDelete(object Id);
        IRepository<T> Repository { get; }

        bool IsUnique(PropertyUniqueDTO dto);
    }
}
