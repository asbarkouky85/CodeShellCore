using CodeShellCore.Data;
using System;

namespace Asga
{
    public interface IAsgaModel : IModel<long>,IEditable
    {
        DateTime? CreatedOn { get; set; }
        long? CreatedBy { get; set; }
        DateTime? UpdatedOn { get; set; }
        long? UpdatedBy { get; set; }
    }
}
