using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.EntityFramework
{
    public class KeyRepository<T, TContext, TPrime> : Repository<T, TContext>, IKeyRepository<T, TPrime>
         where T : class, IEntity<TPrime>

        where TContext : DbContext
    {
        public KeyRepository(TContext con) : base(con)
        {
        }

        //protected virtual IQueryable<Named<TPrime>> QueryNamed(IQueryable<T> q = null)
        //{
        //    q = q ?? Loader;
        //    if (typeof(T).Implements(typeof(INamed<TPrime>)))
        //    {
        //        return ((IQueryable<INamed<TPrime>>)q).Select(d => new Named<TPrime> { Id = d.Id, Name = d.Name });
        //    }
        //    else
        //    {
        //        return q.Select(d => new Named<TPrime> { Id = d.Id, Name = "(" + d.Id + ")" });
        //    }
        //}

        protected virtual IQueryable<Named<object>> QueryNamed(IQueryable<T> q = null)
        {
            q = q ?? Loader;
            if (typeof(T).Implements(typeof(INamed<TPrime>)))
            {
                return ((IQueryable<INamed<TPrime>>)q).Select(d => new Named<object> { Id = d.Id, Name = d.Name });
            }
            else
            {
                return q.Select(d => new Named<object> { Id = d.Id, Name = "(" + d.Id + ")" });
            }
        }

        public override TValue GetValue<TValue>(object id, Expression<Func<T, TValue>> ex)
        {
            return Loader.Where(d => d.Id.Equals(id)).Select(ex).FirstOrDefault();
        }

        public override T FindSingle(object id)
        {
            return Loader.Where(d => d.Id.Equals(id)).FirstOrDefault();
        }

        public virtual Task<T> FindSingleAsync(object id)
        {
            return Loader.Where(d => d.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public override TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id)
        {
            return Loader.Where(d => d.Id.Equals(id)).Select(exp).FirstOrDefault();
        }

        public override TR FindSingleAndMap<TR>(object id)
        {
            var q = Loader.Where(d => d.Id.Equals(id));
            return QueryDto<TR>(q).FirstOrDefault();
        }

        public virtual void DeleteByKey(TPrime id)
        {
            var m = Activator.CreateInstance<T>();
            m.Id = id;
            DbContext.Entry(m).State = EntityState.Deleted;
        }

        public override void DeleteById(object id)
        {
            var m = Activator.CreateInstance<T>();
            m.Id = (TPrime)id;
            DbContext.Entry(m).State = EntityState.Deleted;
        }

        public override void Merge(T obj)
        {
            if (Loader.Any(d => d.Id.Equals(obj.Id)))
                Update(obj);
            else
                Add(obj);
        }

        public override bool IdExists(object ob)
        {
            return Loader.Any(d => d.Id.Equals(ob));
        }

        public bool IdExistsById(TPrime id)
        {
            return Loader.Any(d => d.Id.Equals(id));
        }

        public override void Delete(Expression<Func<T, bool>> ex)
        {
            var ids = GetValues(d => d.Id, ex);
            foreach (var id in ids)
                DeleteById(id);
        }

        public override IEnumerable<Named<object>> FindAsLookup(string collectionId = null)
        {
            return QueryNamed().OrderBy(d => d.Name).ToList();
        }

        public override IEnumerable<Named<object>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex)
        {
            return QueryNamed(Loader.Where(ex)).OrderBy(d => d.Name).ToList();
        }

        public DeleteResult CanDeleteById(TPrime id)
        {
            return CanDelete(id);
        }



        public TR FindSingleAndMapById<TR>(TPrime id) where TR : class
        {
            return FindSingleAndMap<TR>(id);
        }

        public T FindSingleById(TPrime id)
        {
            return FindSingle(id);
        }

        public TValue GetValueById<TValue>(TPrime id, Expression<Func<T, TValue>> ex)
        {
            return GetValue(id, ex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="updateAction">args-->(fromList,fromDb)</param>
        /// <returns></returns>
        public override async Task MergeAsync(IEnumerable<T> list, Action<T, T> updateAction = null)
        {
            var dbList = await Loader.ToListAsync();
            foreach (var item in list)
            {
                var entity = dbList.FirstOrDefault(e => e.Id.Equals(item.Id));
                if (entity == null)
                {
                    Add(item);
                }
                else
                {
                    updateAction?.Invoke(item, entity);
                }
            }
        }

        public Task<T> FindAsync(TPrime id)
        {
            return FindSingleAsync(id);
        }
    }
}
