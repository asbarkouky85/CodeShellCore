using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public interface IRazorTheme
    {
        
        int DefaultControlGroupSize { get; }

        string LayoutBase { get; }
        string ControlGroupTemplate { get; }
        string LocalizableControlGroupTemplate { get; }
        string LabelGroupTemplate { get; }
        string CellTemplate { get; }
        string HeaderCellTemplate { get; }
        bool SortingInTables { get; }
        string SmallBtnClass { get; }

        string GetButtonClass(BtnClass type);
        string GetTemplate(string componentName);
        string GetCell(CellTypes types);
        string GetInputControl(InputControls cont);
        string GetInputControl(string cont);
    }
}
