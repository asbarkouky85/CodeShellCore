using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Data.Helpers;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using CodeShellCore.DependencyInjection;
using System.Data.SqlClient;
using CodeShellCore.Linq;

namespace CodeShellCore.Data.EntityFramework
{
    /// <summary>
    /// Unit of work is a container for all the repositories
    /// </summary>
    public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        protected InstanceStore<IRepository> Store;
        protected IServiceProvider _provider;
        protected virtual Type GenericRepositoryType { get; }
        protected virtual bool UseChangeColumns { get { return false; } }

        protected TContext DbContext { get; private set; }
        public virtual Action<ChangeLists> OnBeforeSave { get { return null; } }
        public virtual Action<ChangeLists> OnSaveSuccess { get { return null; } }
        public bool IsDisposed { get; private set; } = false;
        public event EventHandler<ChangeLists> Saving;

        public UnitOfWork(IServiceProvider provider)
        {
            _provider = provider;
            DbContext = _provider.GetService<TContext>();
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
            if (GenericRepositoryType != null)
            {
                var t = GenericRepositoryType.MakeGenericType(typeof(T), typeof(TContext));
                return (IRepository<T>)Store.GetInstance(t);
            }
            return Store.GetInstance<Repository<T, TContext>>();
        }

        protected virtual void FillChangeColumns(ChangeLists lst)
        {
            if (!UseChangeColumns)
                return;
            object uId = _provider.GetCurrentUser()?.UserId;
            long? userId = null;

            if (uId is string && long.TryParse((string)uId, out long id))
            {
                userId = id;
            }
            else if (uId != null)
            {
                userId = (long?)uId;
            }

            foreach (IChangeColumns mod in lst.Added)
            {
                if (userId != null)
                {
                    mod.CreatedBy = userId;
                    mod.UpdatedBy = userId;
                }

                mod.CreatedOn = DateTime.Now;
                mod.UpdatedOn = DateTime.Now;
            }

            foreach (IChangeColumns mod in lst.Updated)
            {
                if (userId != null)
                    mod.UpdatedBy = userId;
                mod.UpdatedOn = DateTime.Now;
            }
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

            if (OnBeforeSave != null || OnSaveSuccess != null || Saving != null || UseChangeColumns)
            {
                lst = GetChangeSet();
                OnBeforeSave?.Invoke(lst);
                Saving?.Invoke(this, lst);
                FillChangeColumns(lst);
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
                res = CustomizeException(ex, failMessage);
            }
            return res;
        }

        public void Dispose()
        {
            DbContext.Dispose();
            IsDisposed = true;
        }

        SqlException GetSqlException(Exception ex)
        {
            if (ex is SqlException)
                return (SqlException)ex;
            else if (ex.InnerException != null)
                return GetSqlException(ex.InnerException);
            else
                return null;
        }

        public virtual SubmitResult CustomizeException(Exception ex, string failMessage = null)
        {
            var res = new SubmitResult
            {
                Code = 101,
                Message = failMessage
            };
            res.SetException(ex);

            var sqlException = GetSqlException(ex);
            if (sqlException != null)
            {
                if (sqlException.Number == 547)
                {
                    DeleteResult deleteResult = res.MapToResult<DeleteResult>();
                    deleteResult.Code = 547;
                    deleteResult.CanDelete = false;
                    deleteResult.TableName = SqlInterpreter.N00547(sqlException.Message)[5].Split('.')[1];
                    return deleteResult;
                }
            }

            return res;
        }
    }
}
