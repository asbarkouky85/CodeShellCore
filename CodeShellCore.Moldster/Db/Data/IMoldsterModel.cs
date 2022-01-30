using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public interface IMoldsterModel : IModel<long>, IChangeColumns
    {
    }
}
