using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public interface IEntityLinkedUser
    {
        Dictionary<string, IEnumerable<long>> EntityLinks { get; set; }
    }
}
