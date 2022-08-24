using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Data.Helpers
{
    public abstract class ChangeSet
    {
        public static ChangeSet<T> Create<T>(IEnumerable<T> lst) where T : class, IEditable
        {
            ChangeSet<T> set = new ChangeSet<T>();
            if (lst == null)
            {
                lst = new List<T>();
            }
            foreach(var item in lst)
            {
                switch (item.State)
                {
                    case ChangeStates.Added:
                        set.Added.Add(item);
                        break;
                    case ChangeStates.Modified:
                        set.Updated.Add(item);
                        break;
                    case ChangeStates.Removed:
                        set.Deleted.Add(item);
                        break;
                }
            }
            return set;
        }


    }
    public class ChangeSet<T> : ChangeSet where T : class
    {
        public ChangeSet()
        {
            Added = new List<T>();
            Updated = new List<T>();
            Deleted = new List<T>();
        }
        public List<T> Added { get; private set; }
        public List<T> Updated { get; private set; }
        public List<T> Deleted { get; private set; }

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
