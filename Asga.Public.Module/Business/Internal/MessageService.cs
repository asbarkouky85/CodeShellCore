using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data.Helpers;

namespace Asga.Public.Business.Internal
{

    public class MessageService : PublicEntityService<Message>
    {
        private readonly IPublicUnit _unit;
        public MessageService(IPublicUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public override SubmitResult Create(Message obj)
        {
            if (_unit.UserAccessor.IsUser)
            {
                obj.UserId = _unit.UserAccessor.User.GetUserIdAsLong();
            }
            if (obj.Attachments != null)
                _unit.AttachmentRepository.SaveChangesFor(obj, obj.Attachments, "messages");
            return base.Create(obj);
        }

        public override SubmitResult Update(Message obj)
        {
            if (obj.Attachments != null)
                _unit.AttachmentRepository.SaveChangesFor(obj, obj.Attachments, "messages");
            return base.Update(obj);
        }

        public override Message GetSingle(object id)
        {

            var m = base.GetSingle(id);
            if (m != null)
            {
                m.Attachments = _unit.AttachmentRepository.GetFor(m);
                var dt = _unit.MessageRepository.FindSingleAs(d => new { d.MessageType.Name, d.MessageType.BaseType }, e => e.Id == m.Id);
                m.CommentTypeName = dt.Name;
                m.BaseType = dt.BaseType;
            }

            return m;
        }
    }
}
