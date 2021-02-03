using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public class MvcTheme : DefaultTheme
    {
        /// <summary>
        /// "~/ShellComponents/Mvc"
        /// </summary>
        public override string BasePath { get { return "~/ShellComponents/Mvc"; } }
        
    }
}
