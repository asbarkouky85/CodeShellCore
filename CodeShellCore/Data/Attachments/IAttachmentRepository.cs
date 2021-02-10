using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;

namespace CodeShellCore.Data.Attachments
{
    public interface IAttachmentRepository<T> : IRepository<T> where T : class, IAttachmentModel
    {
        IEnumerable<T> GetFor<TParent>(TParent model, string serviceUrl = "") where TParent : class, IModel<long>;
        void SaveChangesFor<TParent>(TParent model, IEnumerable<T> lst) where TParent : class, IModel<long>;
    }
}
