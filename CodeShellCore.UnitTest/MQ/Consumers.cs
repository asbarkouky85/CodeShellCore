using Asga.Auth;
using CodeShellCore.MQ;
using CodeShellCore.MQ.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
