using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class ControlDTO
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public int Accessibilty { get; set; }
        public string Identifier { get; set; }
        public string ControlType { get; set; }
        public CollectionDTO Collection { get; set; }
        public List<ControlDTO> Children { get; set; }
        public List<Validator> DefaultValidators { get; set; }
        public List<Validator> CustomValidators { get; set; }


        public ControlDTO()
        {
            Children = new List<ControlDTO>();
        }
    }
}
