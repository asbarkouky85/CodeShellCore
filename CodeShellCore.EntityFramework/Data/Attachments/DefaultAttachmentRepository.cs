using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;

namespace CodeShellCore.Data.Attachments
{
    public class DefaultAttachmentRepository<T, TContext> : Repository_Int64<T, TContext>, IAttachmentRepository<T>
        where T : class, IAttachmentEntity, IEntity<long>
        where TContext : DbContext
    {

        public DefaultAttachmentRepository(TContext con) : base(con)
        {
        }

        public virtual IEnumerable<T> GetFor<TParent>(TParent model) where TParent : class, IEntity<long>
        {
            string t = typeof(TParent).Name;
            var lst = Loader.Where(d => d.EntityId == model.Id && d.EntityType == t).ToList();
            return lst;
        }

        public virtual void SaveChangesFor<TParent>(TParent model, IEnumerable<T> lst, string folder = null) where TParent : class, IEntity<long>
        {
            string t = typeof(TParent).Name;
            var s = ChangeSet.Create(lst);
            foreach (var item in s.Added)
            {
                //if(FileUtils.SaveTemp(item.File,))
                //item.FilePath = item.File?.SaveFile(folder ?? "");
                item.EntityId = model.Id;
                item.EntityType = t;
            }
            s.Apply(this);
        }
    }
}
