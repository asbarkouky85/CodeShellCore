using CodeShellCore.Cli;
using CodeShellCore.Http.Pushing;
using System;
using System.Drawing;

namespace CodeShellCore.Moldster.Tracing
{
    public class MessagePusherOutputService : IOutputWriter
    {
        ColorSetter setter;

        int cursorLeft = 0;
        private readonly IMessagePusher<IOutputMessageSender> pusher;
        private readonly IPushingSessionManager man;
        string connectionId;

        public MessagePusherOutputService(IMessagePusher<IOutputMessageSender> pusher, IPushingSessionManager man)
        {
            this.pusher = pusher;
            this.man = man;
            connectionId = man.GetConnectionId();
        }
        public void Write(string v, bool replaceLast = false)
        {
            cursorLeft += v.Length;
            var notificationDTO = new NotificationDTO();
            notificationDTO.Payload = v;
            notificationDTO.IsNew = false;
            notificationDTO.Color = GetColor();
            notificationDTO.ReplaceLast = replaceLast;
            pusher.Publish(d => d.SendMessage(notificationDTO), new[] { connectionId });
        }

        public void WriteLine(bool replaceLast = false)
        {
            cursorLeft = 0;
            var notificationDTO = new NotificationDTO();
            notificationDTO.Payload = null;
            notificationDTO.IsNew = true;
            notificationDTO.Color = GetColor();
            notificationDTO.ReplaceLast = replaceLast;
            pusher.Publish(d => d.SendMessage(notificationDTO), new[] { connectionId });
        }

        public void WriteLine(string v, bool replaceLast = false)
        {
            if (v != null)
                cursorLeft += v.Length;
            var notificationDTO = new NotificationDTO();
            notificationDTO.Payload = v;
            notificationDTO.IsNew = true;
            notificationDTO.Color = GetColor();
            notificationDTO.ReplaceLast = replaceLast;

            pusher.Publish(d => d.SendMessage(notificationDTO), new[] { connectionId });
            cursorLeft = 0;
        }

        public ColorSetter Set(ConsoleColor yellow)
        {
            setter = new ColorSetter(yellow);
            return setter;
        }

        public void GotoColumn(int column)
        {
            int current = cursorLeft;
            int dest = column * 8;

            if (dest > current)
            {
                double tabs_dec = (dest - current) / (double)8;
                int tabs = (int)tabs_dec;
                if (tabs < tabs_dec)
                    tabs += 1;
                string t = "";
                for (var i = 0; i < tabs; i++)
                {
                    t += "\t";
                    cursorLeft += 8;
                }
                Write(t);
            }
        }

        public string GetColor()
        {
            ConsoleColor col;
            if (setter == null || setter.IsDisposed)
                col = ConsoleColor.White;
            else
                col = setter.CurrentColor;

            var str = col.ToString();
            Color clr = Color.FromName(str);

            var h = clr.ToHexCode();
            return h;
        }
    }
}
