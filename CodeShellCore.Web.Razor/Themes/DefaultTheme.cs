using CodeShellCore.Helpers;


namespace CodeShellCore.Web.Razor.Themes
{
    public class DefaultTheme : IRazorTheme
    {
        /// <summary>
        /// "~/ShellComponents"
        /// </summary>
        public virtual string LayoutBase { get { return "~/ShellComponents"; } }
        /// <summary>
        /// 6
        /// </summary>
        public virtual int DefaultControlGroupSize { get { return 6; } }
        public virtual string ControlGroupTemplate { get { return "~/ShellComponents/Containers/ControlGroup.cshtml"; } }
        public virtual string LocalizableControlGroupTemplate { get { return "~/ShellComponents/Containers/LocalizableControlGroup.cshtml"; } }
        public virtual string LabelGroupTemplate { get { return "~/ShellComponents/Containers/LabelGroup.cshtml"; } }
        public virtual string CellTemplate { get { return "~/ShellComponents/Containers/Cell.cshtml"; } }
        public virtual string HeaderCellTemplate { get { return "~/ShellComponents/TableCells/HeaderCell.cshtml"; } }

        public virtual bool SortingInTables { get { return true; } }

        public string SmallBtnClass => "btn-sm";

        public virtual string GetButtonClass(BtnClass type)
        {
            return "btn-" + type.ToString().ToLower();
        }

        public virtual string GetCell(CellTypes types)
        {
            return "~/ShellComponents/TableCells/" + types.ToString() + ".cshtml";
        }

        public virtual string GetInputControl(InputControls cont)
        {
            return "~/ShellComponents/InputControls/" + cont.ToString() + ".cshtml";
        }

        public virtual string GetInputControl(string cont)
        {
            return "~/ShellComponents/InputControls/" + cont + ".cshtml";
        }

        public virtual string GetTemplate(string componentName)
        {
            return Utils.CombineUrl("~/ShellComponents/Components/", componentName + ".cshtml");
        }
    }
}
