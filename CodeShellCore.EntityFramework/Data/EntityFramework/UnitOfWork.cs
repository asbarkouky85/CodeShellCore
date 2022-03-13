using CodeShellCore.Data.Auditing;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Mapping;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Text.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CodeShellCore.Data.EntityFramework
{
    /// <summary>
    /// Unit of work is a container for all the repositories
    /// </summary>
    public abstract class UnitOfWork<TContext> : DefaultUnitOfWork, IUnitOfWork where TContext : DbContext
    {
        bool _userObtained = false;
        bool _obtainingUser = false;
        static bool _resourcesObtained = false;
        static bool _obtainingResources = false;
        IAuthorizableUser _authorizable;
        static string[] _collectionResources = new string[0];

        protected virtual bool UseChangeColumns { get { return false; } }
        protected virtual bool UseCollectionPermission => false;

        protected virtual TContext DbContext { get; private set; }
        protected virtual IQueryProjector Projector { get; private set; }
        /// <summary>
        /// override to process changes before saving
        /// </summary>
        public virtual Action<ChangeLists> OnBeforeSave { get { return null; } }
        /// <summary>
        /// override to proccess changes after saving
        /// </summary>
        public virtual Action<ChangeLists> OnSaveSuccess { get { return null; } }

        public override event EventHandler<ChangeLists> Saving;

        public UnitOfWork(IServiceProvider provider) : base(provider)
        {
            DbContext = _provider.GetService<TContext>();
            Projector = _provider.GetService<IQueryProjector>();
        }

        private bool _hasCollections(string res)
        {
            if (!_resourcesObtained && !_obtainingResources)
            {
                _obtainingResources = true;
                var unit = _provider.GetService<ISecurityUnit>();
                if (unit != null)
                    _collectionResources = unit.ResourceRepository.GetResourcesWithCollections();

                _obtainingResources = false;
                _resourcesObtained = true;
            }
            return _collectionResources.Contains(res);
        }

        public override ChangeLists GetChangeSet()
        {
            IEnumerable<EntityEntry> entries = DbContext.ChangeTracker.Entries();
            return new ChangeLists
            {
                Updated = entries.Where(d => d.State == EntityState.Modified).Select(d => d.Entity).ToList(),
                Added = entries.Where(d => d.State == EntityState.Added).Select(d => d.Entity).ToList(),
                Deleted = entries.Where(d => d.State == EntityState.Deleted).Select(d => d.Entity).ToList()
            };
        }

        public override void EnableJsonLoading()
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public override IRepository GetRepositoryFor(Type i)
        {
            var req = typeof(IRepository<>).MakeGenericType(i);
            var repo = _provider.GetService(req) as IRepository;
            if (repo != null)
                return repo;
            var gen = GenericRepositoryType ?? typeof(Repository<,>);
            var t = gen.MakeGenericType(i, typeof(TContext));

            var inst = Store.GetInstance(t);
            inst.Projector = Projector;
            return inst;
        }

        public IKeyRepository<T, TPrime> GetRepositoryFor<T, TPrime>() where T : class, IModel<TPrime>
        {
            IKeyRepository<T, TPrime> inst;
            if (GenericKeyRepositoryType != null)
            {
                var t = GenericKeyRepositoryType.MakeGenericType(typeof(T), typeof(TContext));
                inst = (IKeyRepository<T, TPrime>)Store.GetInstance(t);
            }
            else
            {
                inst = Store.GetInstance<KeyRepository<T, TContext, TPrime>>();
            }
            inst.Projector = Projector;
            return inst;
        }

        public override ICollectionRepository<T> GetCollectionRepositoryFor<T>()
        {
            ICollectionRepository<T> inst;
            if (GenericCollectionRepositoryType != null)
            {
                var t = GenericCollectionRepositoryType.MakeGenericType(typeof(T), typeof(TContext));
                inst = (ICollectionRepository<T>)Store.GetInstance(t);
            }
            else
            {
                inst = Store.GetInstance<ICollectionEFRepository<T, TContext>>(r => appendCollectionId(r));
            }
            inst.Projector = Projector;
            return inst;
        }

        private bool _obtainUser()
        {
            if (!_userObtained)
            {
                _obtainingUser = true;
                var x = UserAccessor.User;
                if (x is IAuthorizableUser)
                    _authorizable = x as IAuthorizableUser;
                _userObtained = true;
                _obtainingUser = false;
            }
            return _authorizable != null;
        }

        protected virtual void appendCollectionId(IRepository r)
        {
            if (!UseCollectionPermission || IgnorePermissions || _obtainingUser || !(r is ICollectionRepository))
                return;

            var repo = r as ICollectionRepository;
            var res = EntityToResource(repo.EntityName);

            if (!_hasCollections(res))
                return;

            if (!_obtainUser())
                return;

            if (_authorizable.Permissions.TryGetValue(res, out DataAccessPermission perm))
                repo.CollectionId = perm.CollectionId;

        }

        public override IRepository<T> GetRepositoryFor<T>()
        {
            var i = typeof(T);
            var repo = _provider.GetService<IRepository<T>>();
            if (repo != null)
            {
                appendCollectionId(repo);
                return repo;
            }
            IRepository<T> inst;
            if (GenericRepositoryType != null)
            {
                var t = GenericRepositoryType.MakeGenericType(typeof(T), typeof(TContext));
                inst = (IRepository<T>)Store.GetInstance(t);
            }
            else
            {
                inst = Store.GetInstance<Repository<T, TContext>>(r => appendCollectionId(r));
            }

            inst.Projector = Projector;
            return inst;
        }


        protected virtual void FillChangeColumns(ChangeLists lst)
        {
            if (!UseChangeColumns)
                return;
            long? userId = _provider.GetCurrentUser()?.GetUserIdAsLong();

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
        public override SubmitResult SaveChanges(string successMessage = null, string failMessage = null)
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

        public override void Dispose()
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

        public override T GetRepository<T>()
        {
            var repo = Store.GetInstance<T>(r => appendCollectionId(r));
            repo.Projector = Projector;
            return repo;
        }
    }
}
