using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public class EditableEntityDto<TPrime> : EntityDto<TPrime>, IDetailObject<TPrime>
    {
        public string State { get; set; }
    }
}
