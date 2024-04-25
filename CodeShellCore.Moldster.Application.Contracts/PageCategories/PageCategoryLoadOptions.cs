using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryLoadOptions : PagedListRequestDto
    {
        public long? DomainId { get; set; }
    }
}
