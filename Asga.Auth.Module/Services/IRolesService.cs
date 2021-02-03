using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;

namespace Asga.Auth.Services
{
    public interface IRolesService : IEntityService<Role>
    {
        EditingDTO<Role> GetSingleEditingDTO(object id);
    }
}