using Asga.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Common.Data
{
    public interface IAsgaUnit
    {
        IAsgaRepository<T> GetAsgaRepositoryFor<T>() where T : class, IAsgaModel;
    }
}
