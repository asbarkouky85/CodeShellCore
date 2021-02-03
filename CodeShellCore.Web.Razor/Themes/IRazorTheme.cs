using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public interface IRazorTheme
    {

        int DefaultControlGroupSize { get; }

        string BasePath { get; }
        string DefaultControlGroupTemplate { get; }
        string CellTemplate { get; }
        string HeaderCellTemplate { get; }
        bool SortingInTables { get; }
        string SmallBtnClass { get; }

        string GetControlGroupTemplate(InputControls type, bool localizable=false);
        string GetControlGroupTemplate(string inputType, bool localizable=false);
        string GetButtonClass(BtnClass type);
        string GetTemplate(string componentName);
        string GetCell(CellTypes types);
        string GetInputControl(InputControls cont);
        string GetInputControl(string cont);
    }
}
