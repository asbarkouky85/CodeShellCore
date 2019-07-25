using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;

namespace CodeShellCore.Services.Attachments
{
    public interface IAttachmentRepository<T> : IAdapterRepository<T, IAttachmentModel> where T : class, IAttachmentModel
    {
        IEnumerable<T> GetFor<TParent>(TParent model, string serviceUrl = "") where TParent : class, IModel<long>;
        void SaveChangesFor<TParent>(TParent model, IEnumerable<T> lst) where TParent : class, IModel<long>;
    }
}
