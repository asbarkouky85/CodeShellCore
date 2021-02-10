using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authentication
{
    public class DomainUser
    {
        public string Domain { get; set; }
        public string UserName { get; set; }

        public DomainUser(string fullName)
        {
            string[] splt = fullName.Split(new char[] { '\\' });
            if (splt.Length > 1)
            {
                Domain = splt.Length > 0 ? splt[0] : null;
                UserName = splt.Length > 1 ? splt[1] : null;
            }
            else
            {
                Domain = null;
                UserName = splt[0];
            }

        }
    }
}
