using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories.Dtos
{

    public class PageCategoryParameterDTO : IEditable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string DefaultValue { get; set; }
        public string State { get; set; }

    }
}
