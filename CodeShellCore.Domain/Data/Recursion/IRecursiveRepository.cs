using CodeShellCore.Data;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Recursion
{
    public interface IRecursiveRepository<T> : IRepository<T> where T : class, IRecursiveModel
    {
        IEnumerable<RecursionModel> GetRecursionModels();
        IEnumerable<RecursionModel> GetRecursionModels(Expression<Func<T, bool>> filter);
        
        IEnumerable<T> GetChildren(object prime);

        IEnumerable<T> GetChildren(object prime, Expression<Func<T, bool>> filter);
        IEnumerable<T> GetRooted(Expression<Func<T, bool>> filter);
        void DeleteAllSubs(object prime);

    }
}
