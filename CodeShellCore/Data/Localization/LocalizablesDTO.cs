using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Localization
{
    public class LocalizablesDTO : IEditable
    {
        public string State { get; set; }
        public int LangId { get; set; }
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }

    public class LocalizableItem { public string ColumnName { get; set; } public string Value { get; set; } }

    public class LocalizablesLoader
    {
        public int LocaleId { get; set; }
        public IEnumerable<LocalizableItem> Items { get; set; }
    }

    //public partial class Localizable : ILocalizable
    //{
    //    public long Id { get; set; }
    //    public long EntityId { get; set; }
       
    //    public string EntityType { get; set; }
    //    public int LocaleId { get; set; }
      
    //    public string ColumnName { get; set; }
    //    public string Value { get; set; }
     
    //    public DateTime? CreatedOn { get; set; }
    //    public long? CreatedBy { get; set; }

    //    public DateTime? UpdatedOn { get; set; }
    //    public long? UpdatedBy { get; set; }
    //    public string State { get; set ; }
    //}
}
