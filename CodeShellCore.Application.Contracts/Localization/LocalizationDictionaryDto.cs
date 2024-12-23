using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localization
{
    public class LocalizationDictionaryDto
    {
        public Dictionary<string, string> Words { get; set; }
        public Dictionary<string, string> Pages { get; set; }
        public Dictionary<string, string> Columns { get; set; }
        public Dictionary<string, string> Messages { get; set; }
    }
}
