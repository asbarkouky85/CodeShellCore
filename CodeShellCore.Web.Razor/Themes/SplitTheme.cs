
using CodeShellCore.Helpers;
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

        public override string DefaultControlGroupTemplate => BasePath + "/Containers/ControlGroup_Split.cshtml";
        public override string LocalizableControlGroupTemplate => BasePath + "/Containers/LocalizableControlGroup_Split.cshtml";
        public override int DefaultControlGroupSize => 6;
        public override string LabelGroupTemplate => BasePath + "/Containers/LabelGroup_Split.cshtml";
    }
}
