using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files
{
    public interface IDimension : IComparable<IDimension>
    {
        int Width { get; }
        int Height { get; }
    }
}
