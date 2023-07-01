using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public class EntityDto<TPrime> : IEntityDto<TPrime>
    {
        public TPrime Id { get; set; }
    }
}
