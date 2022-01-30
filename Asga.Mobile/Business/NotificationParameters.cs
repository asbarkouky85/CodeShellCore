using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Mobile.Business
{
    public class NotificationParameters
    {
        public long EntityId { get; set; }
        public string[] BodyParams { get; set; } = new string[0];
        public string[] TitleParams { get; set; } = new string[0];
        public string Image { get; set; }
    }
}
