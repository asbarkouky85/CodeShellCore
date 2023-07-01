using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Data.Helpers
{
    public abstract class ChangeSet
    {
        protected abstract void SetAdded(IEnumerable lst);
        protected abstract void SetUpdated(IEnumerable lst);
        protected abstract void SetDeleted(IEnumerable lst);
        public static ChangeSet<T> Create<T>(IEnumerable<T> lst) where T : class, IEditable
        {
            ChangeSet<T> set = new ChangeSet<T>();
            if (lst == null)
            {
                lst = new List<T>();
            }
               
            set.SetAdded(lst.Where(d => d.State == ChangeStates.Added).ToList());
            set.SetUpdated(lst.Where(d => d.State == ChangeStates.Modified).ToList());
            set.SetDeleted(lst.Where(d => d.State == ChangeStates.Removed).ToList());

            return set;
        }
    }
    public class ChangeSet<T> : ChangeSet where T : class
    {
        public ICollection<T> Added { get; private set; } = new List<T>();
        public ICollection<T> Updated { get; private set; } = new List<T>();
        public ICollection<T> Deleted { get; private set; } = new List<T>();

        protected override void SetAdded(IEnumerable lst)
        {
            Added = lst as ICollection<T>;
        }

        protected override void SetUpdated(IEnumerable lst)
        {
            Updated = lst as ICollection<T>;
        }

        protected override void SetDeleted(IEnumerable lst)
        {
            Deleted = lst as ICollection<T>;
        }

        public void Apply(IRepository<T> repo)
        {
            foreach (T item in Added)
                repo.Add(item);

            foreach (T item in Updated)
                repo.Update(item);

            foreach (T item in Deleted)
                repo.Delete(item);
        }


    }
}
