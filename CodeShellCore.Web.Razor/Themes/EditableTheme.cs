using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Themes
{
    public class EditableTheme : IRazorTheme
    {
        private Dictionary<string, string> _controlTemplates = new Dictionary<string, string>();

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

        public string BasePath { get { return _getValue("LayoutBase", _inner.BasePath); } set { _setValue("LayoutBase", value); } }

        public string CellTemplate { get { return _getValue("CellTemplate", _inner.CellTemplate); } set { _setValue("CellTemplate", value); } }

        public string HeaderCellTemplate { get { return _getValue("HeaderCellTemplate", _inner.HeaderCellTemplate); } set { _setValue("HeaderCellTemplate", value); } }

        public bool SortingInTables { get { return _getValue("SortingInTables", _inner.SortingInTables); } set { _setValue("SortingInTables", value); } }

        public string SmallBtnClass { get { return _getValue("SmallBtnClass", _inner.SmallBtnClass); } set { _setValue("SmallBtnClass", value); } }

        public string DefaultControlGroupTemplate { get { return _getValue("DefaultControlGroupTemplate", _inner.DefaultControlGroupTemplate); } set { _setValue("DefaultControlGroupTemplate", value); } }

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

        public void SetControlGroupTemplate(InputControls type, bool? localizable, string temp)
        {
            var index = "E__" + type.ToString();
            if (localizable == null)
            {
                _controlTemplates[index] = temp;
                _controlTemplates[index + "__LOC"] = temp;
            }
            else
            {
                index = index + (localizable.Value ? "__LOC" : "");
            }

        }

        public void SetControlGroupTemplate(string inputControl, bool? localizable, string temp)
        {
            var index = inputControl;
            if (localizable == null)
            {
                _controlTemplates[index] = temp;
                _controlTemplates[index + "__LOC"] = temp;
            }
            else
            {
                index = index + (localizable.Value ? "__LOC" : "");
            }

        }

        public string GetControlGroupTemplate(InputControls type, bool localizable=false)
        {
            var index = "E__" + type.ToString() + (localizable ? "__LOC" : "");
            if (_controlTemplates.TryGetValue(index, out string t))
                return t;
            return DefaultControlGroupTemplate;
        }

        public string GetControlGroupTemplate(string inputType, bool localizable=false)
        {
            var index = inputType + (localizable ? "__LOC" : "");
            if (_controlTemplates.TryGetValue(index, out string t))
                return t;
            return DefaultControlGroupTemplate;
        }
    }
}
