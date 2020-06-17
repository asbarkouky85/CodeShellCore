using CodeShellCore.Data;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public class DataService<T> : ServiceBase where T :class,IUnitOfWork
    {
        protected readonly T Unit;
        public DataService(T unit)
        {
            Unit = unit;
        }
    }
}
