using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Razor
{
    public class Accessibility
    {
        private int access;
        public bool Read { get { return access > 0; } }
        public bool Write { get { return access > 1; } }
        public Accessibility(int perm)
        {
            access = perm;
        }
    }
}
