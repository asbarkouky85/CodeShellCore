using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Linq
{
    public static class ApplicationSharedLinqExtensions
    {
        public static void PresistState<T, TPrime>(this IEnumerable<DTO<T, TPrime>> lst)
            where T : class, IEditable
        {
            lst.ForEach(d => d.Entity.State = d.State);
        }

        public static List<Named<TPrime>> GetNamedList<T, TPrime>(this IRepository<T> repo, Expression<Func<T, TPrime>> expression) where T : class, INamed<TPrime>
        {
            return repo.FindAs(e => new Named<TPrime> { Id = e.Id, Name = e.Name }).OrderBy(d => d.Name).ToList();
        }



        public static ChangeSet<T> ApplyChanges<T>(this IRepository<T> repo, IEnumerable<T> lst) where T : class, IEditable
        {
            ChangeSet<T> set = ChangeSet.Create(lst);
            set.Apply(repo);
            if (set.Added.Count() == 0 && set.Updated.Count() == 0 && set.Deleted.Count() == 0)
                set = null;
            return set;
        }

        public static ChangeSet<TEntity> ApplyChanges<TDto, TEntity>(this IRepository<TEntity> repo, IEnumerable<TDto> lst, IObjectMapper mapper)
            where TDto : class, IEditable<long>
            where TEntity : class, IModel<long>
        {
            ChangeSet<TDto> set = ChangeSet.Create(lst);
            ChangeSet<TEntity> entitySet = new ChangeSet<TEntity>();

            foreach (TDto item in set.Added)
            {
                var add = mapper.Map<TDto, TEntity>(item);
                repo.Add(add);
                entitySet.Added.Add(add);
            }

            foreach (TDto item in set.Updated)
            {
                var update = repo.FindSingle(item.Id);
                mapper.Map(item, update);
                repo.Update(update);
                entitySet.Updated.Add(update);
            }

            foreach (TDto item in set.Deleted)
            {
                var deleted = mapper.Map<TDto, TEntity>(item);
                repo.Delete(deleted);
                entitySet.Deleted.Add(deleted);
            }

            if (set.Added.Count() == 0 && set.Updated.Count() == 0 && set.Deleted.Count() == 0)
                set = null;
            return entitySet;
        }
    }
}
