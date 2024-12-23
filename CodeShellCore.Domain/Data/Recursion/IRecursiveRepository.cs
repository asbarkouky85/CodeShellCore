using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Recursion
{
    public interface IRecursiveRepository<T, TRec> : IRepository<T>
        where T : class, IRecursiveModel<T>
        where TRec : class, IRecursiveModel<TRec>
    {
        Task<IEnumerable<TRec>> GetRecursionModels();
        Task<IEnumerable<TRec>> GetRecursionModels(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetChildren(object prime);

        Task<IEnumerable<T>> GetChildren(object prime, Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetRooted(Expression<Func<T, bool>> filter);
        Task DeleteAllSubs(object prime);

    }
}
