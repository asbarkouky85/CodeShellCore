using CodeShellCore.Helpers;

namespace CodeShellCore.FileServer
{
    public class FileServerBaseModel : AuditedEntity<long>
    {
        public FileServerBaseModel()
        {
            Id = Utils.GenerateID();
        }

    }
}
