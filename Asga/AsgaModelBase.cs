﻿using CodeShellCore.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asga
{
    public class AsgaModelBase : IEditable
    {
        [NotMapped]
        public string State { get; set; }
    }
}
