using System;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public interface ITempFileService
    {
        Task CleanUp(DateTime createdBefore);
    }
}