using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class ButtonNgInput : NgInput
    {
        public string FunctionName { get; set; }
        public string Arguments { get; set; }
        public BtnClass BtnClassEnum { get; set; }
        public string IconClass { get; set; }

        public string BtnClass
        {
            get { return RazorUtils.GetButtonClass(BtnClassEnum); }
        }

        public string Icon
        {
            get
            {
                return IconClass == null ? "" : $"<i class=\"{IconClass}\"></i>";
            }
        }
    }
}
