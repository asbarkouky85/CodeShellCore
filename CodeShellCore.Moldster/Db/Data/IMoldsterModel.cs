using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IMoldsterModel : IModel<long>
    {
        DateTime? CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
    }
}
