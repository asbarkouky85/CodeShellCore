using System.Collections.Generic;

namespace CodeShellCore.Data.Localization
{
    public class LocalizablesLoader
    {
        public int LocaleId { get; set; }
        public IEnumerable<LocalizableItem> Items { get; set; }
    }

    
}
