using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeShellCore.ToolSet.Localization
{
    public class AbpResourceFile
    {
        public string Culture { get; set; }
        public Dictionary<string, string> Texts { get; set; }

        public void AppendKeys(IEnumerable<string> keys)
        {
            
            foreach (var item in keys)
            {
                if (!Texts.Any(d => d.Key == item))
                {
                    Texts[item] = LangUtils.IdToPhrase(item);
                }
            }
            Texts = Texts.OrderBy(e => e.Key).ToDictionary();
        }
    }
}
