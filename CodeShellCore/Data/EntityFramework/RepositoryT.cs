using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Data.EntityFramework
{

    /// <summary>
    /// A repository is treated like a data source for a specific entity where you should be able to add, edit and delete and
    /// also retrieve lists with specific conditions
    /// </summary>
    /// <typeparam name="T">the physical or viewmodel type</typeparam>
    public abstract class Repository<T, TContext> : EFRepository<TContext>, IRepository<T> where T : class where TContext : DbContext
    {
        #region members
        private DbSet<T> _saver;
        private IQueryable<T> _loader;
        private Dictionary<Type, IRepository> repos;

        /// <summary>
        /// DataContext
        /// </summary>
        protected TContext DbContext { get; set; }

        /// <summary>
        /// The DbSet the Add, Edit and Delete uses, for a <see cref="IViewModel"/> will be the DbSet of the original Entity
        /// </summary>
        protected DbSet<T> Saver
        {
            get
            {
                if (_saver == null)
                    _saver = GetSaver();
                return _saver;
            }
        }


        /// <summary>
        /// The main <see cref="Queryable"/> object all the repository data is loaded from
        /// </summary>
        protected IQueryable<T> Loader
        {
            get
            {
                if (_loader == null)
                    _loader = GetLoader();
                return _loader;
            }
        }


        #endregion

        /// <summary>
        /// DbContext is mandetory
        /// </summary>
        /// <param name="con">DbContext is mandetory</param>
        public Repository(TContext con)
        {
            DbContext = con;
            repos = new Dictionary<Type, IRepository>();
        }

        #region Protected

        protected string _entityName { get { return typeof(T).Name; } }

        /// <summary>
        /// Gets the DbSet the Add, Edit and Delete uses, for a <see cref="IViewModel"/> will be the DbSet of the original Entity
        /// </summary>
        /// <remarks>you can override this function change the behavior of the saving source</remarks>
        protected virtual DbSet<T> GetSaver()
        {

            return DbContext.Set<T>();

        }

        /// <summary>
        /// Gets the main <see cref="Queryable"/> object all the repository data is loaded from
        /// </summary>
        /// <remarks>you can override this function change the behavior of the load function</remarks>
        protected virtual IQueryable<T> GetLoader()
        {
            return DbContext.Set<T>();
        }

        protected T GetRepository<TRepo>() where TRepo : class, IRepository
        {
            IRepository repo;
            Type t = typeof(T);
            if (!repos.TryGetValue(t, out repo))
            {
                repo = (IRepository)Activator.CreateInstance(t, DbContext);
                repos[t] = repo;
            }
            return (T)repo;
        }

        #endregion

        public abstract TValue GetValue<TValue>(object id, Expression<Func<T, TValue>> ex);
        public abstract T FindSingle(object id);
        public abstract TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class;
        public abstract void DeleteById(object ob);
        public abstract bool IdExists(object ob);

        /// <summary>
        /// to retrieve all records from the data source with the repository entity
        /// </summary>
        /// <returns>should not be a queryable object</returns>
        public IEnumerable All()
        {
            return Loader.ToList();
        }


        public List<T> GetList()
        {
            return Loader.ToList();
        }


        public abstract void Merge(T obj);

        /// <summary>
        /// set the object to be inserted when <see cref="UnitOfWork.SaveChanges"/> is called
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Add(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("entity");
            Saver.Add(obj);
        }

        public void InsertObject(object ob)
        {
            Add((T)ob);
        }


        /// <summary>
        /// set the object to be deleted when <see cref="UnitOfWork.SaveChanges"/> is called
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Delete(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("entity");
            DbContext.Entry(obj).State = EntityState.Deleted;
        }

        public virtual void Update(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("entity");
            DbContext.Entry(obj).State = EntityState.Modified;
        }

        /// <summary>
        /// counts all records in repository
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return Loader.Count(d => true);
        }

        /// <summary>
        /// counts records after filtering using the filter expression provided
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> exp)
        {
            return Loader.Where(exp).Count(d => true);
        }

        /// <summary>
        /// if records in the repository with the given createria exists
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual bool Exist(Expression<Func<T, bool>> exp)
        {
            return Loader.Any(exp);
        }

        /// <summary>
        /// to retrieve all records from the data source using conditions specified in the <see cref="LoadOptions"/> instance 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>should not be a queryable object</returns>
        public virtual List<T> Find(Expression<Func<T, bool>> exp)
        {
            
            return Loader.Where(exp).ToList();
        }

        public virtual LoadResult<T> Find(ListOptions<T> opts)
        {
            return Loader.LoadWith(opts);
        }

        public virtual List<TR> FindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class
        {
            try
            {
                var q = Loader;
                if (cond != null)
                    q = q.Where(cond);

                if (opts != null)
                    return q.Select(exp).ToListWith(opts);

                return q.Select(exp).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public virtual LoadResult<TR> FindAs<TR>(Expression<Func<T, TR>> exp, ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class
        {
            var q = Loader;
            if (cond != null)
                q = q.Where(cond);

            return q.Select(exp).LoadWith(opts);
        }

        public virtual T FindSingle(Expression<Func<T, bool>> expression)
        {
            return Loader.Where(expression).FirstOrDefault();
        }

        public virtual TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class
        {
            try
            {
                return Loader.Where(expression).Select(exp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public virtual IEnumerable<TValue> GetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter)
        {
            var q = Loader;
            if (filter != null)
                q = q.Where(filter);
            return q.Select(ex).ToList();
        }

        public virtual TValue GetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter)
        {
            return Loader.Where(filter).Select(ex).FirstOrDefault();
        }

        public IEnumerable<TValue> GetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null)
        {
            var q = Loader;
            if (filter != null)
                q = q.Where(filter);
            return q.OrderBy(order).Select(ex).ToList();
        }

        public abstract void Delete(Expression<Func<T, bool>> ex);

        public List<TR> FindAs<TR>(Expression<Func<T, TR>> ex, IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class
        {
            var q = Loader;
            foreach(var filter in filtes)
                q = q.Where(filter);
            return q.Select(ex).ToList();
        }

        public abstract DeleteResult CanDelete(object id);

         public List<T> GetByExperession/*<TR,TVal>*/(IEnumerable<Expression<Func<T, bool>>> filtres)/* Func<T, TVal> GroupColumn, Func<T, TR> selction*/
        {
            var q = Loader;
            foreach (var filter in filtres)
                q = q.Where(filter);
           // var de = q.ToLookup(GroupColumn, selction);
            return q.ToList();
        }
    }
}

