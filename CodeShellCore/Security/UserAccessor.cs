using CodeShellCore.Security.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Security
{
    public class UserAccessor : IUserAccessor
    {
        bool dataIsObtained = false;
        object blocker = new object();
        private IUser _user;
        private readonly IServiceProvider provider;

        public UserAccessor(IServiceProvider provider)
        {
            blocker = new object();
            this.provider = provider;
        }
        public IUser User
        {
            get
            {
                return _getUserData();
            }
        }

        public string UserId { get; set; }

        public bool IsUser => !string.IsNullOrEmpty(UserId);
        public bool IsClient => !string.IsNullOrEmpty(ClientId);

        public string ClientId { get; set; }

        public T UserAs<T>() where T : class, IUser
        {
            return (T)User;
        }

        public void Set(IUser user)
        {
            _user = user;
            UserId = user.UserId;
        }

        private IUser _getUserData()
        {
            lock (blocker)
            {
                if (dataIsObtained)
                    return _user;

                if (UserId != null && _user == null)
                {
                    _user = provider.GetService<IUserDataService>().GetUserData(UserId);
                }
                dataIsObtained = true;

            }
            return _user;
        }
    }
}
