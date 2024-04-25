using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Extensions.Data
{
    public static class DataExtensions
    {
        public static List<T> GetChangedItems<T>(this IEnumerable<T> lst) where T : class, IEditable
        {
            return lst.Where(e => e.IsChanged()).ToList();
        }

        public static bool IsChanged(this IEditable ob)
        {
            return ob.State == ChangeStates.Added || ob.State == ChangeStates.Removed || ob.State == ChangeStates.Modified;
        }

        public static List<TEntity> ApplyChangesWithAddUpdateFunction<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper, Func<TDto, TEntity> addFunction, Action<TDto, TEntity> updateFunction = null)
            where TEntity : IEntity<Guid>
            where TDto : class, IDetailObject<Guid>
        {
            return ApplyChangesWithAddUpdateFunctionGeneric<TEntity, TDto, Guid>(repo, items, mapper, addFunction, updateFunction);
        }

        public static List<TEntity> ApplyChangesLongWithAddUpdateFunction<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper, Func<TDto, TEntity> addFunction, Action<TDto, TEntity> updateFunction = null)
            where TEntity : IEntity<long>
            where TDto : class, IDetailObject<long>
        {
            return ApplyChangesWithAddUpdateFunctionGeneric<TEntity, TDto, long>(repo, items, mapper, addFunction, updateFunction);
        }

        public static List<TEntity> ApplyChangesIntWithAddUpdateFunction<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper, Func<TDto, TEntity> addFunction, Action<TDto, TEntity> updateFunction = null)
           where TEntity : IEntity<int>
           where TDto : class, IDetailObject<int>
        {
            return ApplyChangesWithAddUpdateFunctionGeneric<TEntity, TDto, int>(repo, items, mapper, addFunction, updateFunction);
        }

        public static List<TEntity> ApplyChangesInt<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
            where TEntity : class, IEntity<int>
            where TDto : class, IDetailObject<int>
        {
            return ApplyChangesGeneric<TEntity, TDto, int>(repo, items, mapper);
        }

        public static List<TEntity> ApplyChanges<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
            where TEntity : class, IEntity<long>
            where TDto : class, IDetailObject<long>
        {
            return ApplyChangesGeneric<TEntity, TDto, long>(repo, items, mapper);
        }

        public static List<TEntity> ApplyChangesGuid<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
            where TEntity : class, IEntity<Guid>
            where TDto : class, IDetailObject<Guid>
        {
            return ApplyChangesGeneric<TEntity, TDto, Guid>(repo, items, mapper);
        }

        public static List<TEntity> ApplyChangesNoId<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, Func<TEntity, TDto, bool> finder, IObjectMapper mapper)
            where TEntity : class
            where TDto : class, IEditable
        {
            List<TEntity> lst = new List<TEntity>();
            foreach (var item in items)
            {
                switch (item.State)
                {
                    case ChangeStates.Added:
                        var added = mapper.Map<TDto, TEntity>(item);
                        repo.Add(added);
                        lst.Add(added);
                        break;
                    case ChangeStates.Modified:
                        var updated = repo.FirstOrDefault(e => finder(e, item));
                        if (updated != null)
                        {
                            mapper.Map(item, updated);
                            lst.Add(updated);
                        }
                        break;
                    case ChangeStates.Removed:
                        var deleted = repo.FirstOrDefault(e => finder(e, item));
                        repo.Remove(deleted);
                        break;
                    case ChangeStates.Attached:
                        var noAction = mapper.Map<TDto, TEntity>(item);
                        lst.Add(noAction);
                        break;
                }
            }
            return lst;
        }

        internal static List<TEntity> ApplyToRepoGeneric<TEntity, TDto, TPrime>(this IRepository<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
             where TEntity : class, IEntity<TPrime>
             where TDto : class, IDetailObject<TPrime>
        {
            List<TEntity> lst = new List<TEntity>();
            foreach (var item in items)
            {
                switch (item.State)
                {
                    case ChangeStates.Added:
                        var added = mapper.Map<TDto, TEntity>(item);
                        repo.Add(added);
                        lst.Add(added);
                        break;
                    case ChangeStates.Modified:
                        var updated = repo.FindSingle(e => e.Id.Equals(item.Id));
                        if (updated != null)
                        {
                            mapper.Map(item, updated);
                            lst.Add(updated);
                        }
                        break;
                    case ChangeStates.Removed:
                        repo.DeleteById(item.Id);
                        break;
                    case ChangeStates.Attached:
                        var noAction = mapper.Map<TDto, TEntity>(item);
                        lst.Add(noAction);
                        break;
                }
            }
            return lst;
        }

        internal static List<TEntity> ApplyChangesGeneric<TEntity, TDto, TPrime>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
             where TEntity : class, IEntity<TPrime>
             where TDto : class, IDetailObject<TPrime>
        {
            List<TEntity> lst = new List<TEntity>();
            foreach (var item in items)
            {
                switch (item.State)
                {
                    case ChangeStates.Added:
                        var added = mapper.Map<TDto, TEntity>(item);
                        repo.Add(added);
                        lst.Add(added);
                        break;
                    case ChangeStates.Modified:
                        var updated = repo.FirstOrDefault(e => e.Id.Equals(item.Id));
                        if (updated != null)
                        {
                            mapper.Map(item, updated);
                            lst.Add(updated);
                        }
                        break;
                    case ChangeStates.Removed:
                        var deleted = repo.FirstOrDefault(e => e.Id.Equals(item.Id));
                        repo.Remove(deleted);
                        break;
                    case ChangeStates.Attached:
                        var noAction = mapper.Map<TDto, TEntity>(item);
                        lst.Add(noAction);
                        break;
                }
            }
            return lst;
        }

        internal static List<TEntity> ApplyChangesWithAddUpdateFunctionGeneric<TEntity, TDto, TPrime>(
            this ICollection<TEntity> repo,
            IEnumerable<TDto> items,
            IObjectMapper mapper,
            Func<TDto, TEntity> addFunction = null,
            Action<TDto, TEntity> updateFunction = null)
            where TEntity : IEntity<TPrime>
            where TDto : class, IDetailObject<TPrime>
        {
            List<TEntity> lst = new List<TEntity>();
            foreach (var item in items)
            {
                switch (item.State)
                {
                    case ChangeStates.Added:

                        if (addFunction != null)
                        {
                            var added = addFunction(item);
                            lst.Add(added);
                        }
                        else
                        {
                            var added = mapper.Map<TDto, TEntity>(item);
                            repo.Add(added);
                            lst.Add(added);
                        }

                        break;
                    case ChangeStates.Modified:
                        var updated = repo.FirstOrDefault(e => e.Id.Equals(item.Id));

                        if (updated != null)
                        {
                            if (updateFunction != null)
                            {
                                updateFunction(item, updated);
                            }
                            else
                            {
                                mapper.Map(item, updated);
                            }

                            lst.Add(updated);
                        }
                        break;
                    case ChangeStates.Removed:
                        var deleted = repo.FirstOrDefault(e => e.Id.Equals(item.Id));
                        repo.Remove(deleted);
                        break;
                    case ChangeStates.Attached:
                        var noAction = mapper.Map<TDto, TEntity>(item);
                        lst.Add(noAction);
                        break;
                }
            }
            return lst;
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
            where TDto : class, IDetailObject<long>
            where TEntity : class, IEntity<long>
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
                if(update != null)
                {
                    mapper.Map(item, update);
                    repo.Update(update);
                    entitySet.Updated.Add(update);
                }
                
            }

            foreach (TDto item in set.Deleted)
            {
                var deleted = repo.FindSingle(item.Id);
                if(deleted != null)
                {
                    repo.Delete(deleted);
                    entitySet.Deleted.Add(deleted);
                }
                
            }

            if (set.Added.Count() == 0 && set.Updated.Count() == 0 && set.Deleted.Count() == 0)
                set = null;
            return entitySet;
        }
    }
}
