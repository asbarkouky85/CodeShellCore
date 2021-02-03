using System.Collections.Generic;
using Asga.Auth.Dto;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;

namespace Asga.Auth.Services
{
    public interface IUsersService : IEntityService<User>
    {
        SubmitResult ChangePassword(CodeShellCore.Security.Authentication.ChangePasswordDTO dto);
        SubmitResult CheckIsUserExist(User dto);
        string GetRoleNameById(long id);
        EditingDTO<User> GetSingleEditingDTO(object id);
        User GetUserByUserName(string userName);
        Role GetUserRole(long id);
        Role GetUserRoleFromDb(long id);
        List<User> GetUsersByRoleId(long Roleid);
        SubmitResult ResetPassword(ResetDTO dto);
        SubmitResult ResetPassword_SendMail(string email, string hostAddress);
        long SoftDelete(long id, bool isDeleted);
    }
}