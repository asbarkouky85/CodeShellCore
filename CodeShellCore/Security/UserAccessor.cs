
namespace CodeShellCore.Security
{
    public class UserAccessor : IUserAccessor
    {
        public IUser User { get; set; }

        public T UserAs<T>() where T : class, IUser
        {
            return (T)User;
        }
    }
}
