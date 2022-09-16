using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public class EditableEntityDto<TPrime> : EntityDto<TPrime>, IEditable<TPrime>
    {
        public string State { get; set; }
    }
}
