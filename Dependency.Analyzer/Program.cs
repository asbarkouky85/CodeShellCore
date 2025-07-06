using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency.Analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<string>
            {
                
            };

            var items_tsk = DependencyAnalyzer.Read(list);
            items_tsk.Wait();

            DependencyAnalyzer.Generate("C:\\_git\\bejs-core-new", items_tsk.Result).Wait();
        }
    }
}
