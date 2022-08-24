using CodeShellCore.Data;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Data.Services
{
    public class DataService<T> : ServiceBase where T : class, IUnitOfWork
    {
        protected readonly T Unit;
        protected readonly IObjectMapper Mapper;
        public DataService(T unit)
        {
            Unit = unit;
            Mapper = unit.ServiceProvider.GetService<IObjectMapper>();
        }
    }
}
