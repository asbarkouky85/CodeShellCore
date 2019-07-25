using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;

using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;

namespace CodeShellCore.Services.Attachments
{
    public class DefaultAttachmentRepository<T, TContext> : AdapterRepository_Int64<T, IAttachmentModel, TContext>, IAttachmentRepository<T>
        where T : class, IAttachmentModel, IModel<long>
        where TContext : DbContext
    {
        public DefaultAttachmentRepository(TContext con) : base(con) { }

        public IEnumerable<T> GetFor<TParent>(TParent model, string serviceUrl = "") where TParent : class, IModel<long>
        {
            string t = typeof(TParent).Name;
            var lst = Loader.Where(d => d.EntityId == model.Id && d.EntityType == t).ToList();
            foreach (var item in lst)
                item.LoadFile(serviceUrl);
            return lst;
        }

        public virtual void SaveChangesFor<TParent>(TParent model, IEnumerable<T> lst) where TParent : class, IModel<long>
        {
            string t = typeof(TParent).Name;
            var s = ChangeSet.Create(lst);
            foreach (var item in s.Added)
            {

                item.EntityId = model.Id;
                item.EntityType = t;
            }
            s.Apply(this);
        }
    }
}
