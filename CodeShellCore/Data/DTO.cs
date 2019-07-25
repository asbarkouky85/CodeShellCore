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

    public abstract class DTO<T, TPrim> : DTO, IDTO<T> where T : class
    {
        public TPrim Id { get; set; }
        public T Entity { get; set; }
    }
}
