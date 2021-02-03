using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Security;
using CodeShellCore.Text.Localization;
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
        string TranslateIfMobile(string message_code, params string[] parameters);
        bool IgnorePermissions { get; set; }
        ChangeLists GetChangeSet();
        void EnableJsonLoading();
        /// <summary>
        /// can be used to set up an action that runs before saving data
        /// </summary>
        event EventHandler<ChangeLists> Saving;
        IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// contains current locale requested and can be used to translate (Word,Column,Page or Message)
        /// </summary>
        ILocaleTextProvider Strings { get; }
        
        /// <summary>
        /// contains current user data
        /// </summary>
        IUserAccessor UserAccessor { get; }
        /// <summary>
        /// has the data of the client the user is requesting from, like is it mobile
        /// </summary>
        ClientData ClientData { get; }
        /// <summary>
        /// uses registered types to return a generic repository for the entity T
        /// </summary>
        /// <typeparam name="T">entity class </typeparam>
        /// <returns><see cref="IRepository{T}"/></returns>
        IRepository<T> GetRepositoryFor<T>() where T : class;
        IRepository GetRepositoryFor(Type t);
        ICollectionRepository<T> GetCollectionRepositoryFor<T>() where T : class;
        T GetRepository<T>() where T : class, IRepository;
        IRepository GetRepository(Type t);
        SubmitResult SaveChanges(string successMessage = null, string faileMessage = null);
    }
}
