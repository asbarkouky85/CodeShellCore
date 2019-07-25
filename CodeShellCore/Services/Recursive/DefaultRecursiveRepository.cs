using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Lookups;


namespace CodeShellCore.Services.Recursive
{
    public class DefaultRecursiveRepository<T, TContext> : AdapterRepository_Int64<T, IRecursiveModel, TContext>, IRecursiveRepository<T>
         where T : class, IRecursiveModel
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

        protected virtual Expression<Func<T, RecursionModel>> RecusiveModelExpression
        {
            get
            {
                return x => new RecursionModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Chain = x.Chain,
                    NameChain = x.NameChain,
          
                };
            }
        }

        public void DeleteAllSubs(object prime)
        {
            var del = Loader.Where(d => d.Chain.Contains("|" + prime + "|") && d.Id != (long)prime).ToList();
            foreach (var it in del)
                Saver.Remove(it);
        }

        public virtual IEnumerable<T> GetChildren(object prime)
        {
            var q = Loader.Where(d => d.Chain != null && d.Chain.Contains("|" + prime + "|") && !d.Id.Equals(prime));
            return q.ToList();
        }

        public override void Delete(T obj)
        {
            var children = GetChildren(obj.Id);
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
                var children = GetChildren(obj.Id);
                foreach (var ch in children)
                    DbContext.Entry(ch).State = EntityState.Modified;
            }

            base.Update(obj);
        }

        public virtual IEnumerable<RecursionModel> GetRecursionModels()
        {
            return Loader.Select(RecusiveModelExpression).ToList();
        }

        public virtual IEnumerable<T> GetRooted(Expression<Func<T, bool>> filter)
        {
            var required = Loader.Where(filter).AsQueryable();
            return QueryRooted(required).ToList();
        }

        public virtual IEnumerable<RecursionModel> GetRecursionModels(Expression<Func<T, bool>> filter)
        {
            return Loader.Where(filter).Select(RecusiveModelExpression).ToList();
        }

        public virtual IEnumerable<T> GetChildren(object prime, Expression<Func<T, bool>> filter)
        {
            var q = Loader.Where(d => d.Chain != null && d.Chain.Contains("|" + prime + "|") && !d.Id.Equals(prime));
            q = q.Where(filter);

            return q.ToList();
        }
    }
}
