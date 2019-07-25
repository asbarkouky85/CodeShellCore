using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor
{
    public class ComponentNames
    {
        public const string CalendarTextBox = "InputControls/CalendarTextBox";
        public const string CheckBox = "InputControls/CheckBox";
        public const string DateTimeTextBox = "InputControls/DateTimeTextBox";
        public const string FileTextBox = "InputControls/FileTextBox";
        public const string HijriCalendarTextBox = "InputControls/HijriCalendarTextBox";
        public const string Radio = "InputControls/Radio";
        public const string Select = "InputControls/Select";
        public const string Select_Searchable = "InputControls/Select_Searchable";
        public const string Textarea = "InputControls/Textarea";
        public const string TextBox = "InputControls/TextBox";
        public const string LocalizableTextBox = "InputControls/LocalizableTextBox";
        public const string ButtonCell = "TableCells/ButtonCell";
        public const string HeaderCell = "TableCells/HeaderCell";
        public const string DragCell = "TableCells/DragCell";
        public const string LabelCell = "TableCells/LabelCell";
        public const string WordCell = "TableCells/WordCell";

        public const string SearchGroup = "SearchGroup";
        public const string ControlGroup = "ControlGroup";

        public const string Label = "Label";
    }

    public enum InputControls
    {
        CalendarTextBox, //"InputControls/CalendarTextBox";
        CheckBox, //"InputControls/CheckBox";
        DateTimeTextBox, //"InputControls/DateTimeTextBox";
        FileTextBox, //"InputControls/FileTextBox";
        HijriCalendarTextBox, //"InputControls/HijriCalendarTextBox";
        Radio, //"InputControls/Radio";
        Select, //"InputControls/Select";
        Select_Searchable, //"InputControls/Select_Searchable";
        Textarea, //"InputControls/Textarea";
        TextBox, //"InputControls/TextBox";
        LocalizableTextBox, //"InputControls/LocalizableTextBox";
        Label,
        ListView,
    }

    public enum CellTypes
    {
        DragCell, //"TableCells/DragCell";
        LabelCell, //"TableCells/LabelCell";
    }

    public enum ElementTypes
    {
        TextBox,
        TextBoxWithLabel,
        ComboBox,
        CheckListBox,

    }

    public enum CalendarTypes
    {
        PastDate,
        PastAndFuture,
        FutureDate,
        Custom
    }

    public enum Calendars
    {
        Greg,
        Hijri
    }

    public static class TextBoxTypes
    {
        public const string Text = "text";
        public const string Date = "date";
        public const string Calendar = "calendar";
        public const string Number = "number";
        public const string Password = "password";
        public const string Email = "email";
    }

    public static class Patterns
    {
        public const string Identifier = "[_a-zA-Z][_a-zA-Z0-9]{2,30}";
        public const string Code = "[-_a-zA-Z0-9]{2,50}";
        public const string Email = @"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        public const string Numeric = "[+-]?([0-9]*[.])?[0-9]+";
        public const string UserName = "[_a-zA-Z][_a-zA-Z0-9_.]{2,30}";
    }

    /// <summary>
    /// 
    /// </summary>
    public enum Btn { Delete, Save }
    /// <summary>
    /// 
    /// </summary>
    public enum BtnClass { Primary, Info, Warning, Danger, Default, Success }
}
