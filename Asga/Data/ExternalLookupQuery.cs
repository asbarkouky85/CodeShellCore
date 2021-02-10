using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Dto
{
    public class ExternalLookupQuery
    {
        public string PageCategory { get; set; }
        public Dictionary<string,string> Required { get; set; }
    }
}
