using CodeShellCore.Helpers;


namespace CodeShellCore.Web.Razor.Themes
{
    public class DefaultTheme : IRazorTheme
    {
        /// <summary>
        /// "~/ShellComponents"
        /// </summary>
        public virtual string BasePath { get { return "~/ShellComponents"; } }
        /// <summary>
        /// 6
        /// </summary>
        public virtual int DefaultControlGroupSize { get { return 6; } }
        public virtual string DefaultControlGroupTemplate => BasePath + "/Containers/ControlGroup.cshtml";
        public virtual string LocalizableControlGroupTemplate => BasePath + "/Containers/LocalizableControlGroup.cshtml";
        public virtual string LabelGroupTemplate => BasePath + "/Containers/LabelGroup.cshtml";
        public virtual string CellTemplate => BasePath + "/Containers/Cell.cshtml";
        public virtual string HeaderCellTemplate => BasePath + "/TableCells/HeaderCell.cshtml";

        public virtual bool SortingInTables => true;

        public virtual string SmallBtnClass => "btn-sm";

        public virtual string GetButtonClass(BtnClass type)
        {
            return "btn-" + type.ToString().ToLower();
        }

        public virtual string GetCell(CellTypes types)
        {
            return BasePath + "/TableCells/" + types.ToString() + ".cshtml";
        }

        public virtual string GetControlGroupTemplate(InputControls type, bool localizable = false)
        {
            if (type == InputControls.Label)
                return LabelGroupTemplate;
            else if (localizable)
                return LocalizableControlGroupTemplate;

            return DefaultControlGroupTemplate;
        }

        public virtual string GetControlGroupTemplate(string inputType, bool localizable = false)
        {
            if (localizable)
                return LocalizableControlGroupTemplate;

            return DefaultControlGroupTemplate;
        }

        public virtual string GetInputControl(InputControls cont)
        {
            return BasePath + "/InputControls/" + cont.ToString() + ".cshtml";
        }

        public virtual string GetInputControl(string cont)
        {
            return BasePath + "/InputControls/" + cont + ".cshtml";
        }

        public virtual string GetTemplate(string componentName)
        {
            return BasePath + "/" + componentName + ".cshtml";
        }
    }
}
