using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Data
{
    public interface IEditable
    {
        [NotMapped]
        string State { get; set; }


    }
}
