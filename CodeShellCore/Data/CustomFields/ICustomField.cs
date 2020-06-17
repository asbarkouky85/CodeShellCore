using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;

namespace CodeShellCore.Data.CustomFields
{
  public interface ICustomField : IModel<long>, IEditable
    {
         string EntityType { get; set; }         long EntityId { get; set; }         string Name { get; set; }         string Value { get; set; }

    }
}
