using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asga.Public
{
    public class AsgaPublicModelBase : IEditable
    {
        [NotMapped]
        public string State { get; set; }
    }
    public interface IAsgaPublicModel : IModel<long>, IChangeColumns { }
}
