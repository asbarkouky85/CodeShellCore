using CodeShellCore.MQ;
using CodeShellCore.MQ.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.UnitTest.MQ
{
    public class UserAdded
    {
        public long Id { get; set; }
    }
    public interface ITestUserService
    {
        void UserIsAdded(UserAdded added);
    }
    public class UsersConsumer : IDefaultConsumer<UserAdded>
    {
        public void Consume(ConsumptionContext<UserAdded> cont)
        {
            var s = cont.ServiceProvider.GetRequiredService<ITestUserService>();
            s.UserIsAdded(cont.Message);
        }
    }
}
