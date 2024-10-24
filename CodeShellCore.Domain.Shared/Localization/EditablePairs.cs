using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localization
{
    public class EditablePairs : IEditable
    {
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        public string State { get; set; }
    }
}
