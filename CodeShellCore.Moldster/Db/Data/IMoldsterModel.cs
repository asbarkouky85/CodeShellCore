using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public interface IMoldsterModel : IModel<long>, IChangeColumns
    {
    }
}
