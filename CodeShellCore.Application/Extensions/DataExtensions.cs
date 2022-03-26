using CodeShellCore.Data;
using CodeShellCore.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

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
            where TEntity : IModel<Guid>
            where TDto : class, IEditable<Guid>
        {
            return ApplyChangesWithAddUpdateFunctionGeneric<TEntity, TDto, Guid>(repo, items, mapper, addFunction, updateFunction);
        }

        public static List<TEntity> ApplyChangesLongWithAddUpdateFunction<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper, Func<TDto, TEntity> addFunction, Action<TDto, TEntity> updateFunction = null)
            where TEntity : IModel<long>
            where TDto : class, IEditable<long>
        {
            return ApplyChangesWithAddUpdateFunctionGeneric<TEntity, TDto, long>(repo, items, mapper, addFunction, updateFunction);
        }

        public static List<TEntity> ApplyChangesIntWithAddUpdateFunction<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper, Func<TDto, TEntity> addFunction, Action<TDto, TEntity> updateFunction = null)
           where TEntity : IModel<int>
           where TDto : class, IEditable<int>
        {
            return ApplyChangesWithAddUpdateFunctionGeneric<TEntity, TDto, int>(repo, items, mapper, addFunction, updateFunction);
        }

        public static List<TEntity> ApplyChangesInt<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
            where TEntity : class, IModel<int>
            where TDto : class, IEditable<int>
        {
            return ApplyChangesGeneric<TEntity, TDto, int>(repo, items, mapper);
        }

        public static List<TEntity> ApplyChangesLong<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
            where TEntity : class, IModel<long>
            where TDto : class, IEditable<long>
        {
            return ApplyChangesGeneric<TEntity, TDto, long>(repo, items, mapper);
        }

        public static List<TEntity> ApplyChanges<TEntity, TDto>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
            where TEntity : class, IModel<Guid>
            where TDto : class, IEditable<Guid>
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


        internal static List<TEntity> ApplyChangesGeneric<TEntity, TDto, TPrime>(this ICollection<TEntity> repo, IEnumerable<TDto> items, IObjectMapper mapper)
             where TEntity : class, IModel<TPrime>
             where TDto : class, IEditable<TPrime>
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
            where TEntity : IModel<TPrime>
            where TDto : class, IEditable<TPrime>
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
    }
}
