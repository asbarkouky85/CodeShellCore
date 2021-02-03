using CodeShellCore.Data.Lookups;
using CodeShellCore.Helpers;

namespace Asga.Auth
{
    public partial class App : INamed<long> { }
    public partial class Resource : INamed<long> { }
    public partial class Role : INamed<long>
    {
        public UserRole AddUser(User u)
        {
            var ur = new UserRole { Id = Utils.GenerateID() };
            u.UserRoles.Add(ur);
            UserRoles.Add(ur);
            return ur;
        }
    }
}
