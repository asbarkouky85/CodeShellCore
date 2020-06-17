using CodeShellCore.Helpers;

namespace CodeShellCore.Web.Razor.Themes
{
    public class AngularTheme : IRazorTheme
    {
        public virtual int DefaultControlGroupSize { get { return 6; } }
        public virtual string ControlGroupTemplate { get { return "~/ShellComponents/Angular/Containers/ControlGroup.cshtml"; } }
        public virtual string LocalizableControlGroupTemplate { get { return "~/ShellComponents/Angular/Containers/LocalizableControlGroup.cshtml"; } }
        public virtual string LabelGroupTemplate { get { return "~/ShellComponents/Angular/Containers/LabelGroup.cshtml"; } }
        public virtual string CellTemplate { get { return "~/ShellComponents/Angular/Containers/Cell.cshtml"; } }
        public virtual string HeaderCellTemplate { get { return "~/ShellComponents/Angular/TableCells/HeaderCell.cshtml"; } }

        public string LayoutBase { get { return "~/ShellComponents/Angular"; } }

        public virtual bool SortingInTables => true;

        public string SmallBtnClass => "btn-sm";

        public virtual string GetButtonClass(BtnClass type)
        {
            return "btn-" + type.ToString().ToLower();
        }

        public virtual string GetCell(CellTypes types)
        {
            return "~/ShellComponents/Angular/TableCells/" + types.ToString() + ".cshtml";
        }

        public virtual string GetInputControl(InputControls cont)
        {
            return "~/ShellComponents/Angular/InputControls/" + cont.ToString() + ".cshtml";
        }

        public virtual string GetInputControl(string cont)
        {
            return "~/ShellComponents/Angular/InputControls/" + cont + ".cshtml";
        }

        public virtual string GetTemplate(string componentName)
        {
            return Utils.CombineUrl("~/ShellComponents/Angular/", componentName + ".cshtml");
        }
    }
}
