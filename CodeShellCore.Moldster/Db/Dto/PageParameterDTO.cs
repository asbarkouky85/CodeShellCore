using CodeShellCore.Data;
using CodeShellCore.Text;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Dto
{
    public class PageParameterForJson
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public long PageId { get; set; }
    }
    public class PageCategoryParameterWithPageId
    {
        public string ParameterName { get; set; }
        public string DefaultValue { get; set; }
        public long? LinkedPageId { get; set; }
        public int Type { get; set; }
        public long PageCategoryParameterId { get; set; }
    }
    public class PageParameterDTO : DTO<PageParameter, long>
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeString { get { return TypeEnum.GetString(); } }
        public PageParameterTypes TypeEnum => (PageParameterTypes)Type;
        public string DefaultValue { get; set; }
        public string ViewPath { get; set; }
    }
}
