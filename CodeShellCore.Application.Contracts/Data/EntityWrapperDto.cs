using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public abstract class EntityWrapperDto<T, TPrim> : EntityDto<TPrim>, IDetailObject<TPrim>, IEntityWrapperDto<T> where T : class
    {
        public T Entity { get; set; }
        public string State { get; set; }
    }
}
