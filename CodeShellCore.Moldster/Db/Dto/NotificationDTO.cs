using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class NotificationDTO
    {
        public string Type { get; set; }
        public string Payload { get; set; }
        public string Color { get; set; }
        public bool ReplaceLast { get; set; }
        public bool IsNew { get; set; }
    }
}
