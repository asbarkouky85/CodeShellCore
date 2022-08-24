using CodeShellCore.Data;
using CodeShellCore.Localization;
using CodeShellCore.Text;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Pages
{
    [EntityName("PageParameter")]
    public class PageParameterEditDto : EntityDto<long>
    {
        public PageParameterDto Entity { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeString { get { return TypeEnum.GetString(); } }
        public PageParameterTypes TypeEnum => (PageParameterTypes)Type;
        public string DefaultValue { get; set; }
        public string ViewPath { get; set; }
        public string State { get; set; }
    }
}
