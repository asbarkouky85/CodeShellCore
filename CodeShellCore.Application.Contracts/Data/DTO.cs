using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public abstract class DTO : IEditable
    {
        public string State { get; set; }
    }
    //public abstract class DTO<T> : DTO, IDTO<T> where T : class
    //{
    //    
    //}

    public abstract class DTO<T, TPrim> : EntityDto<TPrim>,IEditable<TPrim>//, IDTO<T> where T : class
    {
        public T Entity { get; set; }
        public string State { get; set; }
    }
}
