using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ChangeLists GetChangeSet();
        void EnableJsonLoading();
        IRepository<T> GetRepositoryFor<T>() where T : class;
        T GetRepository<T>() where T : class,IRepository;
        SubmitResult SaveChanges(string successMessage = null, string faileMessage = null);
    }
}
