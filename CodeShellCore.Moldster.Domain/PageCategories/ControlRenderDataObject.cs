using CodeShellCore.Moldster.Resources;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public class ControlRenderDataObject
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public int Accessibilty { get; set; }
        public string Identifier { get; set; }
        public string ControlType { get; set; }
        public CollectionDataObject Collection { get; set; }
        public List<ControlRenderDataObject> Children { get; set; }
        public long PageId { get; set; }
        public ControlRenderDataObject()
        {
            Children = new List<ControlRenderDataObject>();
        }
    }
}
