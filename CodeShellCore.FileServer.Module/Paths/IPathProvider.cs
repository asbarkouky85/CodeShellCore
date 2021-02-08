using System.Threading.Tasks;

namespace CodeShellCore.FileServer.Paths
{
    public interface IPathProvider
    {
        string TempFolder { get; }
        string RootFolderPath { get; }

    }
}
