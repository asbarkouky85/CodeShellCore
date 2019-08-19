using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Data.Helpers;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;

namespace CodeShellCore.Data.EntityFramework
{
    /// <summary>
    /// Unit of work is a container for all the repositories
    /// </summary>
    public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        protected InstanceStore<IRepository> Store;
        protected IServiceProvider _provider;
        private bool _disposed = false;

        protected TContext DbContext { get; private set; }
        public virtual Action<ChangeLists> OnBeforeSave { get { return null; } }
        public virtual Action<ChangeLists> OnSaveSuccess { get { return null; } }
        public bool IsDisposed { get { return _disposed; } }
        public UnitOfWork()
        {
            Console.WriteLine(this);
            if (Shell.ScopedInjector == null)
            {
               var  Scope = Shell.GetScope();
                _provider = Scope.ServiceProvider;
                
            }
            else
            {
                _provider = Shell.ScopedInjector;
            }
            DbContext =_provider.GetService<TContext>();
            Store = new InstanceStore<IRepository>(_provider);

        }

        public ChangeLists GetChangeSet()
        {
            IEnumerable<EntityEntry> entries = DbContext.ChangeTracker.Entries();
            return new ChangeLists
            {
                Updated = entries.Where(d => d.State == EntityState.Modified).Select(d => d.Entity).ToList(),
                Added = entries.Where(d => d.State == EntityState.Added).Select(d => d.Entity).ToList(),
                Deleted = entries.Where(d => d.State == EntityState.Deleted).Select(d => d.Entity).ToList()
            };
        }

        public void EnableJsonLoading()
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            return Store.GetInstance<T>();
        }

        public T GetEFRepository<T>() where T : EFRepository<TContext>, IRepository
        {
            return Store.GetInstance<T>();
        }

        

        public IRepository GetRepository(Type t)
        {
            return Store.GetInstance(t);
        }

        public IRepository<T> GetRepositoryFor<T>() where T : class
        {
            var i = typeof(T);
            var repo = _provider.GetService<IRepository<T>>();
            if (repo != null)
                return repo;
            return Store.GetInstance<Repository<T, TContext>>();
        }

        /// <summary>
        /// Attempts to submit changes to the data source
        /// </summary>
        /// <returns>if success <see cref="SubmitResult.Code"/> is 0</returns>
        public virtual SubmitResult SaveChanges(string successMessage = null, string failMessage = null)
        {
            SubmitResult res = new SubmitResult();
            string def = Strings.Word(MessageIds.success_message);
            string defFail = Strings.Word(MessageIds.fail_message);
            successMessage = successMessage ?? def;
            failMessage = failMessage ?? defFail;

            ChangeLists lst = null;

            if (OnBeforeSave != null || OnSaveSuccess != null)
            {
                lst = GetChangeSet();
                OnBeforeSave?.Invoke(lst);
            }

            try
            {
                int rows = DbContext.SaveChanges();

                res = new SubmitResult
                {
                    AffectedRows = rows,
                    Code = 0,
                    Message = successMessage
                };

                try
                {
                    OnSaveSuccess?.Invoke(lst);
                }
                catch (Exception ex)
                {
                    res.SetException(ex);
                    res.Code = 0;
                }


            }
            catch (Exception ex)
            {
                res = CustomizeExpetion(ex, failMessage);
            }
            return res;
        }

        public void Dispose()
        {
            DbContext.Dispose();
            _disposed = true;
        }



        public virtual SubmitResult CustomizeExpetion(Exception ex, string failMessage = null)
        {
            var res = new SubmitResult
            {
                Code = 101,
                Message = failMessage
            };
            res.SetException(ex);
            
            return res;
        }
    }
}
