using System.Collections.Generic;

namespace Asga.Auth.Services
{
    public interface IAuthLookupService
    {
        object RolesEdit(Dictionary<string, string> data);
        object UserEdit(Dictionary<string, string> data);
    }
}