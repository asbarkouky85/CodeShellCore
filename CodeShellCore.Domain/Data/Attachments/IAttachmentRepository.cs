using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;

namespace CodeShellCore.Data.Attachments
{
    public interface IAttachmentRepository<T> : IRepository<T> where T : class, IAttachmentEntity
    {
        IEnumerable<T> GetFor<TParent>(TParent model) where TParent : class, IEntity<long>;
        void SaveChangesFor<TParent>(TParent model, IEnumerable<T> lst, string folder = null) where TParent : class, IEntity<long>;
    }
}
