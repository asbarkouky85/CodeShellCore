using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public class EditableTheme : IRazorTheme
    {
        IRazorTheme _inner;
        IDictionary<string, object> _vals = new Dictionary<string, object>();
        public EditableTheme(IRazorTheme th)
        {
            _inner = th;
        }

        private T _getValue<T>(string key, T def)
        {
            if (_vals.TryGetValue(key, out object val))
            {
                return (T)val;
            }
            return def;
        }

        private void _setValue(string key, object val)
        {
            _vals[key] = val;
        }

        public int DefaultControlGroupSize { get { return _getValue("DefaultControlGroupSize", _inner.DefaultControlGroupSize); } set { _setValue("DefaultControlGroupSize", value); } }


        public string LayoutBase { get { return _getValue("LayoutBase", _inner.LayoutBase); } set { _setValue("LayoutBase", value); } }

        public string ControlGroupTemplate { get { return _getValue("ControlGroupTemplate", _inner.ControlGroupTemplate); } set { _setValue("ControlGroupTemplate", value); } }

        public string LocalizableControlGroupTemplate { get { return _getValue("LocalizableControlGroupTemplate", _inner.LocalizableControlGroupTemplate); } set { _setValue("LocalizableControlGroupTemplate", value); } }

        public string LabelGroupTemplate { get { return _getValue("LabelGroupTemplate", _inner.LabelGroupTemplate); } set { _setValue("LabelGroupTemplate", value); } }

        public string CellTemplate { get { return _getValue("CellTemplate", _inner.CellTemplate); } set { _setValue("CellTemplate", value); } }

        public string HeaderCellTemplate { get { return _getValue("HeaderCellTemplate", _inner.HeaderCellTemplate); } set { _setValue("HeaderCellTemplate", value); } }

        public bool SortingInTables { get { return _getValue("SortingInTables", _inner.SortingInTables); } set { _setValue("SortingInTables", value); } }

        public string SmallBtnClass { get { return _getValue("SmallBtnClass", _inner.SmallBtnClass); } set { _setValue("SmallBtnClass", value); } }

        public string GetButtonClass(BtnClass type)
        {
            return _inner.GetButtonClass(type);
        }

        public string GetCell(CellTypes types)
        {
            return _inner.GetCell(types);
        }

        public string GetInputControl(InputControls cont)
        {
            return _inner.GetInputControl(cont);
        }

        public string GetInputControl(string cont)
        {
            return _inner.GetInputControl(cont);
        }

        public string GetTemplate(string componentName)
        {
            return _inner.GetTemplate(componentName);
        }
    }
}
