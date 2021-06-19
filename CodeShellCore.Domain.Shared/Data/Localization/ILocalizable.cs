using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Localization
{
    public interface ILocalizable : IModel<long>, IEditable
    {
        long EntityId { get; set; }
        string EntityType { get; set; }
        int LocaleId { get; set; }
        string ColumnName { get; set; }
        string Value { get; set; }
    }
}
