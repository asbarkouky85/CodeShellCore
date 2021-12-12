using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories.Dtos
{
    public class PageCategoryListDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string BaseComponent { get; set; }
        public string ResourceName { get; set; }
        public string DomainName { get; set; }
        public string Layout { get; set; }
        public string ViewPath { get; set; }
        public long? ResourceId { get; set; }

        public long? DomainId { get; set; }

        public static Expression<Func<PageCategory, PageCategoryListDTO>> Expression
        {
            get
            {
                return e => new PageCategoryListDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    BaseComponent = e.BaseComponent,
                    ResourceName = e.Resource.Name != null ? e.Resource.Name : "",
                    DomainName = e.Domain.Name != null ? e.Domain.Name : "",
                    ResourceId = e.ResourceId,
                    DomainId = e.DomainId,
                    Layout = e.Layout,
                    ViewPath = e.ViewPath
                };
            }
        }
    }
}
