using CodeShellCore.Data;
using CodeShellCore.Data.Attachments;
using CodeShellCore.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Data
{
    public class PublicUnit : UnitOfWork<AsgaPublicContext>, IPublicUnit
    {
        protected override bool UseChangeColumns => true;

        protected override Type GenericRepositoryType => typeof(AsgaPublicRepository<,>);
        protected override Type GenericCollectionRepositoryType => typeof(AsgaPublicRepository<,>);

        public IRepository<PublicContent> PublicContentRepository => GetRepositoryFor<PublicContent>();

        public IAttachmentRepository<Attachment> AttachmentRepository { get => GetRepository<DefaultAttachmentRepository<Attachment, AsgaPublicContext>>(); }

        public IMessageRepository MessageRepository => GetRepository<IMessageRepository>();

        public IHomeSlideRepository HomeSlideRepository => GetRepository<IHomeSlideRepository>();

        public PublicUnit(IServiceProvider provider) : base(provider)
        {
        }
    }
}
