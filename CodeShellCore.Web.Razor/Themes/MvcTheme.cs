using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public class MvcTheme : IRazorTheme
    {
        /// <summary>
        /// "~/ShellComponents/Mvc"
        /// </summary>
        public virtual string LayoutBase { get { return "~/ShellComponents/Mvc"; } }
        /// <summary>
        /// 6
        /// </summary>
        public virtual int DefaultControlGroupSize { get { return 6; } }
        public virtual string ControlGroupTemplate { get { return "~/ShellComponents/Mvc/Containers/ControlGroup.cshtml"; } }
        public virtual string LocalizableControlGroupTemplate { get { return "~/ShellComponents/Mvc/Containers/LocalizableControlGroup.cshtml"; } }
        public virtual string LabelGroupTemplate { get { return "~/ShellComponents/Mvc/Containers/LabelGroup.cshtml"; } }
        public virtual string CellTemplate { get { return "~/ShellComponents/Mvc/Containers/Cell.cshtml"; } }
        public virtual string HeaderCellTemplate { get { return "~/ShellComponents/Mvc/TableCells/HeaderCell.cshtml"; } }

        public virtual bool SortingInTables { get { return true; } }

        public string SmallBtnClass => "btn-sm";

        public virtual string GetButtonClass(BtnClass type)
        {
            return "btn-" + type.ToString().ToLower();
        }

        public virtual string GetCell(CellTypes types)
        {
            return "~/ShellComponents/Mvc/TableCells/" + types.ToString() + ".cshtml";
        }

        public virtual string GetInputControl(InputControls cont)
        {
            return "~/ShellComponents/Mvc/InputControls/" + cont.ToString() + ".cshtml";
        }

        public virtual string GetInputControl(string cont)
        {
            return "~/ShellComponents/Mvc/InputControls/" + cont + ".cshtml";
        }

        public virtual string GetTemplate(string componentName)
        {
            return Utils.CombineUrl("~/ShellComponents/Mvc/Components/", componentName + ".cshtml");
        }
    }
}
