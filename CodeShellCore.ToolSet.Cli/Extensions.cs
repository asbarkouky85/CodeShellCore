using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet
{
    public static class Extensions
    {
        public static string ToFolderPath(this string st)
        {
            st.Replace('/', '\\');
            if (st[st.Length - 1] != '\\')
                st += '\\';
            return st;
        }
    }
}
