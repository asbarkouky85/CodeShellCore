using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Net
{
    public class PublisherRequest
    {
        public ServerRequestTypes Type { get; set; }
        public string FileName { get; set; }
        public string DestinationFolder { get; set; }
        public bool? DeleteFileAfter { get; set; }
    }
}
