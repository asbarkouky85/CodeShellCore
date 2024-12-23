using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Data;

namespace CodeShellCore.Data.Attachments
{
    public interface IAttachmentRepository<T> : IRepository<T> where T : class, IAttachmentEntity
    {
        Task<IEnumerable<T>> GetFor<TParent>(TParent model) where TParent : class, IEntity<long>;
        Task SaveChangesFor<TParent>(TParent model, IEnumerable<T> lst, string folder = null) where TParent : class, IEntity<long>;
    }
}
