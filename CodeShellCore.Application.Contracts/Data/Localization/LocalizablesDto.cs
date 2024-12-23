using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Localization
{
    public class LocalizablesDto : IDetailObject<long>
    {
        public string State { get; set; }
        public int LangId { get; set; }
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        public long Id { get; set; }
        public bool Selected { get; set; }
    }
}
