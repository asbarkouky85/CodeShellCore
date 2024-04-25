using CodeShellCore.MQ;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Extensions;
using CodeShellCore.Data.Events;

namespace CodeShellCore.FileServer.Consumers
{
    public class TempFileConfirmedConsumer : Consumer, IConsumer<TempFileConfirmed>
    {
        IAttachmentFileService Attachments => Store.GetRequiredService<IAttachmentFileService>();
        public Task Consume(ConsumeContext<TempFileConfirmed> context)
        {
            return ConsumeEvent(context.Message, async message =>
            {
                var data = Mapper.Map(message.FileData, new SaveAttachmentRequestDto());
                return await Attachments.SaveAttachment(data);
            }, context.Message.TenantId);
        }
    }
}
