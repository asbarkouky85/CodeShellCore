using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public class PageReferenceDTO
    {
        public long PageId { get; set; }
        public string PageName { get; set; }
        public string PageViewPath { get; set; }

        public long? ReferencedPageId { get; set; }
        public string ReferencedPageName { get; set; }
        public string ReferencedPageViewPath { get; set; }

        public long PageCategoryId { get; set; }
        public string PageCategoryName { get; set; }
        public string PageCategoryViewPath { get; set; }

        public string ParameterName { get; set; }
        public int ReferenceTypeId { get; set; }
        public string ReferenceTypeName => ReferenceTypeId.ToEnumString<PageParameterTypes>();

    }
}
