using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Events
{
    public interface IEntityHandler<T> where T : class
    {

        Task<SubmitResult> Handle(CrudEvent<T> eventData);

    }
}
