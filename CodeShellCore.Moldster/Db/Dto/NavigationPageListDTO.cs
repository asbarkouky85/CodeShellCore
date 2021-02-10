using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class NavigationPageListDTO : IDTO<NavigationPage>
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public long? PageId { get; set; }
        public long NavigationGroupId { get; set; }
        public static Expression<Func<NavigationPage, NavigationPageListDTO>> Expression
        {
            get
            {
                return d => new NavigationPageListDTO
                {
                    Id = d.Id,
                    DisplayOrder = d.DisplayOrder,
                    Name = d.Page.Name,
                    TenantId = d.Page.TenantId,
                    Url = d.Page.ViewPath,
                    PageId = d.PageId,
                    NavigationGroupId = d.NavigationGroupId
                };
            }
        }
    }
}
