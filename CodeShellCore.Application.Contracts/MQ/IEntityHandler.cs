using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.MQ.Events;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ
{
    public interface IEntityHandler<T> : IEntityService<T> where T : class
    {
        SubmitResult Handle(CrudEvent<T> item);

    }
}
