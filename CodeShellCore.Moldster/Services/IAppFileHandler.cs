using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface IAppFileHandler
    {
        void MovePageFiles(string tenantCode, string from, string to);
        void DeletePageFiles(string tenantCode, string viewPath);
    }
}
