using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asga.Mobile
{
    public class AsgaMobileModelBase : IEditable
    {
        [NotMapped]
        public string State { get; set; }
    }
    public interface IAsgaMobileModel : IModel<long>, IChangeColumns { }
}
