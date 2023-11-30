using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data
{
    public interface IKeyRepository<T, TPrime> : IRepository<T> where T : class
    {
        Task<T> FindAsync(TPrime id);
        DeleteResult CanDeleteById(TPrime id);
        bool IdExistsById(TPrime id);
        TR FindSingleAndMapById<TR>(TPrime id) where TR : class;
        T FindSingleById(TPrime id);
        TValue GetValueById<TValue>(TPrime id, Expression<Func<T, TValue>> ex);
        void DeleteByKey(TPrime id);
    }
}
