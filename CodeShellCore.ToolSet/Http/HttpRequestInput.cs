using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.ToolSet.Http
{
    public class HttpRequestInput
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
    }
}
