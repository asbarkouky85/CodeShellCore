using Asga.Public.Data;
using CodeShellCore.Data;
using CodeShellCore.Data.Attachments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public
{
    public interface IPublicUnit : IUnitOfWork
    {
        IRepository<PublicContent> PublicContentRepository { get; }
        IAttachmentRepository<Attachment> AttachmentRepository { get; }
        IMessageRepository MessageRepository { get; }
        IHomeSlideRepository HomeSlideRepository { get; }
    }
}
