using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Linq;
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

        public override async Task<TValue> GetValue<TValue>(object id, Expression<Func<T, TValue>> ex)
        {
            return await Loader.Where(d => d.Id.Equals(id)).Select(ex).FirstOrDefaultAsync();
        }

        public override async Task<T> FindSingle(object id)
        {
            return await Loader.Where(d => d.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual async Task<T> FindSingleAsync(object id)
        {
            return await Loader.Where(d => d.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public override async Task<TR> FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id)
        {
            return await Loader.Where(d => d.Id.Equals(id)).Select(exp).FirstOrDefaultAsync();
        }

        public override async Task<TR> FindSingleAndMap<TR>(object id)
        {
            var q = Loader.Where(d => d.Id.Equals(id));
            return await QueryDto<TR>(q).FirstOrDefaultAsync();
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

        public override async Task<bool> IdExists(object ob)
        {
            return await Loader.AllAsync(d => d.Id.Equals(ob));
        }

        public async Task<bool> IdExistsById(TPrime id)
        {
            return await Loader.AnyAsync(d => d.Id.Equals(id));
        }

        public override async Task Delete(Expression<Func<T, bool>> ex)
        {
            var ids = await Saver.Where(ex).Select(d => d.Id).ToListAsync();
            foreach (var id in ids)
                DeleteById(id);
        }

        public override async Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId = null)
        {
            return await QueryNamed().OrderBy(d => d.Name).ToListAsync();
        }

        public override async Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex)
        {
            return await QueryNamed(Loader.Where(ex)).OrderBy(d => d.Name).ToListAsync();
        }

        public Task<DeleteResult> CanDeleteById(TPrime id)
        {
            return CanDelete(id);
        }



        public async Task<TR> FindSingleAndMapById<TR>(TPrime id) where TR : class
        {
            return await FindSingleAndMap<TR>(id);
        }

        public async Task<T> FindSingleById(TPrime id)
        {
            return await FindSingle(id);
        }

        public async Task<TValue> GetValueById<TValue>(TPrime id, Expression<Func<T, TValue>> ex)
        {
            return await GetValue(id, ex);
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

        public override async Task<PagedResult<Named<object>>> FindAsLookupPaged(PagedListRequest request, string collectionId = null)
        {
            var req = request.GetOptionsFor<Named<object>>();
            return await QueryNamed().ToPagedResultAsync(req);
        }
    }
}
