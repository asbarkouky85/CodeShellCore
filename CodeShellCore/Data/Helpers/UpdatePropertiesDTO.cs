using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class UpdatePropertiesDTO
    {
        public long Id { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
