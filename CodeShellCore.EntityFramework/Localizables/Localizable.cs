using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using CodeShellCore.Data.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localizables
{
    public class Localizable : IModel<long>, ILocalizable, IChangeColumns
    {
        public long EntityId { get; set; }
        public string EntityType { get; set; }
        public int LocaleId { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public long Id { get; set; }
        public string State { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
