using System;
using System.Linq.Expressions;
using Asga.Public.Business;
using CodeShellCore.Data;
using CodeShellCore.Linq;

namespace Asga.Public.Data
{
    public interface IMessageRepository  :IRepository<Message>
    {
        LoadResult<MessageListDTO> FindAsMessageListDTO(ListOptions<MessageListDTO> opt, string collectionId = null, Expression<Func<Message, bool>> filter = null);
    }
}