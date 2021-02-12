using CodeShellCore.Data;
using CodeShellCore.Data.Auditing;
using System;

namespace Asga
{
    public interface IAuthModel : IModel<long>, IEditable, IChangeColumns
    {

    }
}
