﻿using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;

namespace CodeShellCore.Data.CustomFields
{
  public interface ICustomField : IModel<long>, IEditable
    {
         string EntityType { get; set; }

    }
}