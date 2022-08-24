using CodeShellCore.Moldster.Resources;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public class ControlRenderDto
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public int Accessibilty { get; set; }
        public string Identifier { get; set; }
        public string ControlType { get; set; }
        public CollectionDTO Collection { get; set; }
        public List<ControlRenderDto> Children { get; set; }
        //public List<Validator> DefaultValidators { get; set; }
        //public List<Validator> CustomValidators { get; set; }


        public ControlRenderDto()
        {
            Children = new List<ControlRenderDto>();
        }
    }
}
