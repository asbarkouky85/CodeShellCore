using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public interface IEntityWrapperDto<T> : IEntityWrapperDto where T : class
    {
        T Entity { get; set; }
    }

    public interface IEntityWrapperDto<T, TPrime> : IEntityWrapperDto<T>, IDetailObject<TPrime> where T : class
    {

    }
}
