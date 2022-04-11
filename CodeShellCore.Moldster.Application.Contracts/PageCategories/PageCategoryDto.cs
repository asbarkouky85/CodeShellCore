using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryDto : EntityDto<long>
    {
        public string Name { get; set; }
        [StringLength(300)]
        public string BaseComponent { get; set; }
        [StringLength(300)]
        public string ViewPath { get; set; }
        public long? ResourceId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [StringLength(150)]
        public string Layout { get; set; }
        public long? DomainId { get; set; }


        public ICollection<ControlDto> Controls { get; set; }

        public List<PageCategoryParameterDto> PageCategoryParameters { get; set; }

        //public ICollection<Page> Pages { get; set; }

        public string ResourceName { get; set; }
    }
}
