using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using CodeShellCore.Text.Localization;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Data
{
    public interface IUnitOfWork : IDisposable, IAddDistributedEvent
    {
        string TranslateIfMobile(string message_code, params string[] parameters);
        bool IgnorePermissions { get; set; }
        ChangeLists GetChangeSet();
        ChangeLists LastChanges { get; }
        CurrentTenant CurrentTenant { get; }

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
        ICollectionRepository<T> GetCollectionRepositoryFor<T>() where T : class;
        IKeyRepository<T, TPrime> GetRepositoryFor<T, TPrime>() where T : class, IEntity<TPrime>;
        IRepository GetRepository(Type t);
        IRepository GetRepositoryFor(Type t);
        T GetRepository<T>() where T : class, IRepository;

        Task<SubmitResult> SaveChangesAsync(string successMessage = null, string failMessage = null, bool throwException = true);
        SubmitResult SaveChanges(string successMessage = null, string faileMessage = null, bool throwException = false);
    }
}
