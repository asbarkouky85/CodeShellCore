using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public class DataAccessPermission : Permission
    {
        public bool Details => Privilege.FromBit(1);
        public bool Insert => Privilege.FromBit(2);
        public bool Update => Privilege.FromBit(3);
        public bool Delete => Privilege.FromBit(4);

        public DataAccessPermission(int perm) : base(perm)
        {

        }
    }
}
