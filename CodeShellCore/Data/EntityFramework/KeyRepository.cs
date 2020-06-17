﻿using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.EntityFramework
{
    public class KeyRepository<T, TContext, TPrime> : Repository<T, TContext>
         where T : class, IModel<TPrime>
        where TContext : DbContext
    {
        public KeyRepository(TContext con) : base(con)
        {
        }

        public virtual IQueryable<Named<TPrime>> QueryNamed(IQueryable<T> q = null)
        {
            q = q ?? Loader;
            if (typeof(T).Implements(typeof(INamed<TPrime>)))
            {
                return ((IQueryable<INamed<TPrime>>)q).Select(d => new Named<TPrime> { Id = d.Id, Name = d.Name });
            }
            else
            {
                return q.Select(d => new Named<TPrime> { Id = d.Id, Name = "(" + d.Id + ")" });
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

        public override TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id)
        {
            return Loader.Where(d => d.Id.Equals(id)).Select(exp).FirstOrDefault();
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

        public override void Delete(Expression<Func<T, bool>> ex)
        {
            var ids = GetValues(d => d.Id, ex);
            foreach (var id in ids)
                DeleteById(id);
        }
    }
}
