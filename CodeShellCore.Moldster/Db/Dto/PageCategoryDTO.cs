using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class PageCategoryDTO
    {
        public PageCategory Category { get; set; }
        public string Resource { get; set; }
        public string Domain { get; set; }
        public static Expression<Func<PageCategory, PageCategoryDTO>> Expression
        {
            get
            {
                return e => new PageCategoryDTO
                {
                    Category = e,
                    Resource = e.Resource == null ? null : e.Resource.Name,
                    Domain = e.Resource == null ? null : e.Resource.Domain.Name
                };
            }
        }
    }
}
