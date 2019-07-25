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
        public IList Added { get; set; }
        public IList Updated { get; set; }
        public IList Deleted { get; set; }
    }
}
