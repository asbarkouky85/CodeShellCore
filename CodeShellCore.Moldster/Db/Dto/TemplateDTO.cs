using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class TemplateDTO : IDTO<PageCategory>
    {
        public string Name { get; set; }
        public string ViewPath { get; set; }
        public string BaseComponent { get; set; }
        public string ResourceName { get; set; }
        public long ResourceId { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
