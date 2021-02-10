using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Angular
{
    public static class AngularUtils
    {
        public static string ComponentImport(string componentPath, out string name)
        {
            name = componentPath.GetAfterLast("/");
            return "import { " + name + " } from \"./" + componentPath + "\";\n";
        }
    }
}
