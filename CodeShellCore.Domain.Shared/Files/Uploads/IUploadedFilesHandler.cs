using System.Threading.Tasks;

namespace CodeShellCore.Files.Uploads
{
    public interface IUploadedFilesHandler
    {
        void SaveTemp(ITempFileData req, long? type = null, string folder = null, bool db = false);
        void DeleteTmp(ITempFileData tmp);
        string GetUrlById(string id);
        string GetUrlByPath(string id);
        ITempFileData AddToTemp(FileBytes bts, string key);

        string TempRoot { get; }
        string SaveRoot { get; }
    }
}
