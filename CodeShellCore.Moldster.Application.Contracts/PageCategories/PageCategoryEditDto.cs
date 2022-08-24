using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryEditDto
    {
        public PageCategoryDto Category { get; set; }
        public string Resource { get; set; }
        public string ResourceDomain { get; set; }
        public string Domain { get; set; }

    }
}
