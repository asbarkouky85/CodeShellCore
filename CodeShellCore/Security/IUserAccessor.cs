
namespace CodeShellCore.Security
{
    public interface IUserAccessor
    {
        IUser User { get; set; }
        T UserAs<T>() where T : class, IUser;
    }
}
