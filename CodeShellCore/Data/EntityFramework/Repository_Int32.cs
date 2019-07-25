using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Data.EntityFramework
{
    public class Repository_Int32<T, TContext> : Repository<T,TContext> where T : class, IModel<int> where TContext : DbContext
    {
        public Repository_Int32(TContext con) : base(con)
        {
        }

        public override TValue GetValue<TValue>(object id, Expression<Func<T, TValue>> ex)
        {
            return Loader.Where(d => d.Id.Equals(id)).Select(ex).FirstOrDefault();
        }

        public override T FindSingle(object id)
        {
            return Loader.Where(d => d.Id.Equals(id)).FirstOrDefault();
        }

        public override TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id)
        {
            return Loader.Where(d => d.Id.Equals(id)).Select(exp).FirstOrDefault();
        }

        public override void DeleteById(object id)
        {
            var m = Activator.CreateInstance<T>();
            m.Id = (int)id;
            DbContext.Entry(m).State = EntityState.Deleted;
        }

        public override void Merge(T obj)
        {
            if (Loader.Any(d => d.Id == obj.Id))
                Update(obj);
            else
                Add(obj);
        }

        public override bool IdExists(object ob)
        {
            return Loader.Any(d => d.Id.Equals(ob));
        }

        public override void Delete(Expression<Func<T, bool>> ex)
        {
            var ids = GetValues(d => d.Id, ex);
            foreach (var id in ids)
                DeleteById(id);
        }

        public override DeleteResult CanDelete(object id)
        {
            throw new NotImplementedException();
        }
    }
}
