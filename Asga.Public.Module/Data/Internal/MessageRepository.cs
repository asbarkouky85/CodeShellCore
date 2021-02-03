using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Asga.Public.Business;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Linq;
using CodeShellCore.Security;

namespace Asga.Public.Data.Internal
{

    public class MessageRepository : AsgaPublicRepository<Message, AsgaPublicContext>, IMessageRepository
    {
        public MessageRepository(AsgaPublicContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
        {
        }

        protected virtual IQueryable<MessageListDTO> QueryMessageListDTO(IQueryable<Message> q = null)
        {
            q = q ?? Loader;
            return q.Select(d => new MessageListDTO
            {
                BaseTypeId = d.MessageType.BaseType,
                CreatedBy = d.CreatedBy,
                CreatedOn = d.CreatedOn,
                StatusId = d.StatusId,
                Email = d.Email,
                Id = d.Id,
                MessageTypeName = d.MessageType.Name,
                Name = d.Name,
                Phone = d.Phone,
                ReferenceEntity = d.ReferenceEntity,
                ReferenceId = d.ReferenceId,
                ResponseOn = d.ResponseOn,
                Title = d.Title,
                UpdatedBy = d.UpdatedBy,
                UpdatedOn = d.UpdatedOn

            });
        }

        public virtual LoadResult<MessageListDTO> FindAsMessageListDTO(ListOptions<MessageListDTO> opt, string collectionId = null, Expression<Func<Message, bool>> filter = null)
        {
            var q = string.IsNullOrEmpty(collectionId) ? Loader : QueryCollection(collectionId);
            if (filter != null)
                q = q.Where(filter);

            return QueryMessageListDTO(q).LoadWith(opt);
        }
    }
}
