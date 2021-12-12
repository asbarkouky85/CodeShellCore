using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Pages.Dtos
{
    public class PageControlListDTO
    {
        public long Id { get; set; }
        public string PageName { get; set; }
        public string ControlType { get; set; }
        public string ControlIdentifier { get; set; }
        public byte? Accessability { get; set; }
        public string SourceCollectionName { get; set; }

        public bool? Persistent { get; set; }
        public string State { get; set; }
        public bool Selected { get; set; }

        public long PageId { get; set; }
        public long ControlId { get; set; }
        //public long? AccessabilityId { get; set; }
        public long? SourceCollectionId { get; set; }

        public static Expression<Func<PageControl, PageControlListDTO>> Expression
        {
            get
            {
                return e => new PageControlListDTO
                {
                    Id = e.Id,
                    ControlType = e.Control.ControlType,
                    ControlIdentifier = e.Control.Identifier,
                    PageName = e.Page.Name,
                    //Accessability = e.Accessability == 1 ? "Read Only" : (e.Accessability == 2 ? "Read Write" : "Hide") ,
                    SourceCollectionName = e.SourceCollection.Name,
                    Persistent = e.Persistent,
                    ControlId = e.ControlId,
                    PageId = e.PageId,
                    Accessability = e.Accessability,
                    SourceCollectionId = e.SourceCollectionId
                };
            }
        }
    }
}
