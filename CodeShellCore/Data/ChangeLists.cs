using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data
{
    public class ChangeLists
    {
        public IEnumerable<object> Added { get; set; }
        public IEnumerable<object> Updated { get; set; }
        public IEnumerable<object> Deleted { get; set; }
    }
}
