using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public interface IDTO { }
    public interface IDTO<T>:IDTO where T :class
    {
        
    }
}
