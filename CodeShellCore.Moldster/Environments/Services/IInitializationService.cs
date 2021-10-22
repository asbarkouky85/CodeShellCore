using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Environments.Services
{
    public interface IInitializationService
    {
        void AddStaticFiles(bool replace);
        void AddCodeShell(bool replace);
        void AddShellComponents(bool replace);
        void AddBasicFiles(bool replace);
        void AddUiBasicFiles(bool replace);
    }
}
