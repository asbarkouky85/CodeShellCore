using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories.Dtos
{
    public class CategoryBaseComponentDTO
    {
        //  public string ScriptPath { get; internal set; }
        public string ViewPath { get; internal set; }
        public string Name { get; internal set; }
        public string Resource { get; internal set; }
        public string ResourceDomain { get; internal set; }
    }
}
