using CodeShellCore.Data;

namespace CodeShellCore.Files
{
    public interface IHasFileDto
    {
        TempFileDto File { get; set; }
    }
}