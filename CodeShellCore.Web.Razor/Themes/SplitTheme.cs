
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public class SplitTheme : AngularTheme
    {
        public SplitTheme()
        {
        }

        public override string ControlGroupTemplate { get { return "~/ShellComponents/Angular/Containers/ControlGroup_Split.cshtml"; } }
        public override string LocalizableControlGroupTemplate => "~/ShellComponents/Angular/Containers/LocalizableControlGroup_Split.cshtml";
        public override int DefaultControlGroupSize { get { return 6; } }
        public override string LabelGroupTemplate { get { return "~/ShellComponents/Angular/Containers/LabelGroup_Split.cshtml"; } }

        public override string GetButtonClass(BtnClass type)
        {
            if (RazorConfig.Theme is SplitTheme)
                return "btn-"+type.ToString().ToLower();
            return RazorConfig.Theme.GetButtonClass(type);
        }
    }
}
