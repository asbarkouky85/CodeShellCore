using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace CodeShellCore.Data.Recursion
{
    public class DefaultRecursiveRepository<T, TRec, TContext> : Repository_Int64<T, TContext>, IRecursiveRepository<T, TRec>
         where T : class, IRecursiveModel<T>
        where TRec : class, IRecursiveModel<TRec>
         where TContext : DbContext
    {
        protected virtual bool UpdateChildrenOnUpdate { get { return true; } }
        public DefaultRecursiveRepository(TContext con) : base(con) { }

        protected virtual IQueryable<T> QueryRooted(IQueryable<T> q = null)
        {
            q = q ?? Loader;
            return from tn in Loader
                   where (from tin in Loader
                          from tx in q
                          where tx.Chain.Contains("|" + tin.Id + "|")
                          select tin.Id).Contains(tn.Id)
                   select tn;
        }

        public virtual async Task DeleteAllSubs(object prime)
        {
            var del = await Loader.Where(d => d.Chain.Contains("|" + prime + "|") && d.Id != (long)prime).ToListAsync();
            foreach (var it in del)
                Saver.Remove(it);
        }

        public virtual async Task<IEnumerable<T>> GetChildren(object prime)
        {
            var q = Loader.Where(d => d.Chain != null && d.Chain.Contains("|" + prime + "|") && !d.Id.Equals(prime));
            return await q.ToListAsync();
        }

        public override void Delete(T obj)
        {
            var t = GetChildren(obj.Id);
            t.Wait();
            var children = t.Result;
            foreach (var ch in children)
                DbContext.Entry(ch).State = EntityState.Deleted;
            base.Delete(obj);
        }

        public override void Update(T obj)
        {
            obj.Chain = null;
            obj.NameChain = null;
            if (UpdateChildrenOnUpdate)
            {
                var t = GetChildren(obj.Id);
                t.Wait();
                var children = t.Result;
                foreach (var ch in children)
                    DbContext.Entry(ch).State = EntityState.Modified;
            }

            base.Update(obj);
        }

        public virtual async Task<IEnumerable<TRec>> GetRecursionModels()
        {
            return await Projector.Project<T, TRec>(Loader).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetRooted(Expression<Func<T, bool>> filter)
        {
            var required = filter == null ? Loader : Loader.Where(filter).AsQueryable();
            return await QueryRooted(required).ToListAsync();
        }

        public virtual async Task<IEnumerable<TRec>> GetRecursionModels(Expression<Func<T, bool>> filter)
        {
            return await Projector.Project<T, TRec>(Loader.Where(filter)).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetChildren(object prime, Expression<Func<T, bool>> filter)
        {
            var q = Loader.Where(d => d.Chain != null && d.Chain.Contains("|" + prime + "|") && !d.Id.Equals(prime));
            q = q.Where(filter);

            return await q.ToListAsync();
        }
    }
}
