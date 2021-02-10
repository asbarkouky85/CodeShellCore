
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Security
{
    public class UserAccessor : IUserAccessor
    {
        bool dataIsObtained = false;
        object blocker = new object();
        private IUser _user;
        public IUser User
        {
            get
            {
                return _getUserData();
            }
        }

        public UserAccessor()
        {
            blocker = new object();
        }
        public object UserId { get; set; }
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
                    IServiceProvider prov = null;
                    prov = Shell.ScopedInjector ?? Shell.RootInjector;
                    var ser = prov.GetService<IUserDataService>();
                    if (ser != null)
                        _user = ser.GetUserData(UserId);
                }
                dataIsObtained = true;
                
            }
            return _user;
        }
    }
}
