using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.ToolSet.Nuget;

namespace CodeShellCore.ToolSet.Ftp
{
    public class CopyRequest 
    {
        public string FromPath { get; set; }
        public string ToPath { get; set; }
        public bool DestinationIsAFile { get; set; }
        
    }
}
