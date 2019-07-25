
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public class SplitTheme : DefaultTheme, IRazorTheme
    {
        public SplitTheme()
        {
        }

        public override string ControlGroupTemplate { get { return "ControlGroup_Split"; } }

        public override int DefaultControlGroupSize { get { return 6; } }
        public override string LabelGroupTemplate { get { return "LabelGroup_Split"; } }

        public override string GetButtonClass(BtnClass type)
        {
            if (RazorConfig.Theme is SplitTheme)
                return "btn-"+type.ToString().ToLower();
            return RazorConfig.Theme.GetButtonClass(type);
        }
    }
}
