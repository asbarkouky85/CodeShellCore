using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Data.EntityFramework
{

    /// <summary>
    /// A repository is treated like a data source for a specific entity where you should be able to add, edit and delete and
    /// also retrieve lists with specific conditions
    /// </summary>
    /// <typeparam name="T">the physical type</typeparam>
    public abstract class Repository<T, TContext> : IRepository<T> where T : class where TContext : DbContext
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
        public IQueryProjector Projector { get; set; }



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

        public abstract TValue GetValue<TValue>(object id, Expression<Func<T, TValue>> ex);
        public abstract TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class;
        public abstract TR FindSingleAndMap<TR>(object id) where TR : class;

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


        public virtual T FindSingle(object id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public virtual void DeleteById(object ob)
        {
            var d = DbContext.Set<T>().Find(ob);
            if (d != null)
                Saver.Remove(d);
        }
        public abstract bool IdExists(object ob);

        /// <summary>
        /// to retrieve all records from the data source with the repository entity
        /// </summary>
        /// <returns>should not be a queryable object</returns>
        public virtual IEnumerable All()
        {
            return Loader.ToList();
        }


        public virtual List<T> GetList()
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

        public virtual void InsertObject(object ob)
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
            foreach (var filter in filtes)
                q = q.Where(filter);
            return q.Select(ex).ToList();
        }


        public virtual DeleteResult CanDelete(object id)
        {
            if (!IdExists(id))
                return new DeleteResult { CanDelete = true, Code = 0 };
            using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                DeleteResult res = new DeleteResult();
                try
                {
                    DeleteById(id);
                    DbContext.SaveChanges();
                    txscope.Dispose();
                    res.AffectedRows = 0;
                    res.CanDelete = true;
                    res.Code = 0;
                    return res;
                    //txscope.Complete();
                }
                catch (Exception ex)
                {
                    txscope.Dispose();

                    res.AffectedRows = 0;
                    res.CanDelete = false;
                    res.Code = 1;
                    res.ExceptionMessage = ex.Message;
                    int? number = ((dynamic)ex.InnerException)?.Number;
                    string tableName = "";
                    if (number == 547)
                        tableName = SqlInterpreter.N00547(ex.InnerException.Message)[5].Split('.')[1];
                    res.TableName = tableName;
                    // Log error    
                    return res;

                }
            }
        }
        public virtual LoadResult<TR> FindAsSorted<TR, TV>(Expression<Func<T, TR>> exp, Expression<Func<T, TV>> sort, SortDir dir, ListOptions<TR> opts) where TR : class
        {
            if (dir == SortDir.ASC)
                return Loader.OrderBy(sort).Select(exp).LoadWith(opts);
            else
                return Loader.OrderByDescending(sort).Select(exp).LoadWith(opts);
        }

        public TVal GetMax<TVal>(Expression<Func<T, TVal>> exp, Expression<Func<T, bool>> filter = null)
        {
            var q = Loader;
            if (filter != null)
                q = q.Where(filter);
            if (!q.Any())
            {
                return Activator.CreateInstance<TVal>();
            }
            return q.Max(exp);
        }

        public T Merge(Expression<Func<T, bool>> ex, T obj)
        {
            var item = Loader.Where(ex).FirstOrDefault();
            if (item == null)
            {
                item = obj;
                Add(item);
            }
            return item;
        }

        public abstract IEnumerable<Named<object>> FindAsLookup(string collectionId = null);

        public abstract IEnumerable<Named<object>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex);

        public virtual bool FindSingleOrAdd(Expression<Func<T, bool>> ex, T obj, out T existing)
        {
            var item = FindSingle(ex);
            if (item != null)
            {
                existing = item;
                return true;
            }
            else
            {
                Add(obj);
                existing = obj;
                return false;
            }
        }

        protected virtual IQueryable<TDto> QueryDto<TDto>(IQueryable<T> q = null)
        {
            return Projector.Project<T, TDto>(q ?? Loader);
        }

        public List<TR> FindAndMap<TR>(Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class
        {
            var q = Loader;
            if (cond != null)
            {
                q = q.Where(cond);
            }
            var dtoq = QueryDto<TR>(q);
            if (opts != null)
            {
                return dtoq.ToListWith(opts);
            }
            return dtoq.ToList();
        }

        public List<TR> FindAndMap<TR>(IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class
        {
            var q = Loader;
            foreach (var ex in filtes)
                q = q.Where(ex);
            return QueryDto<TR>(q).ToList();
        }

        public LoadResult<TR> FindAndMap<TR>(ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class
        {
            var q = Loader;
            if (cond != null)
            {
                q = q.Where(cond);
            }
            return QueryDto<TR>(q).LoadWith(opts);
        }

        public TR FindSingleAndMap<TR>(Expression<Func<T, bool>> expression) where TR : class
        {
            var q = Loader.Where(expression);
            return QueryDto<TR>(q).FirstOrDefault();
        }
        //
        // Summary:
        //     Used to get a IQueryable that is used to retrieve entities from entire table.
        //     One or more
        //
        // Parameters:
        //   includes:
        //     A list of include expressions.
        //
        // Returns:
        //     IQueryable to be used to select entities from database
        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes)
        {

            var dbSet = DbContext.Set<T>();

            IQueryable<T> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query ?? dbSet;

        }

        public abstract Task MergeAsync(IEnumerable<T> list, Action<T, T> updateAction = null);

        public async Task InsertAsync(T tmp)
        {
            if (tmp == null)
                throw new ArgumentNullException("entity");
            await Saver.AddAsync(tmp);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> value)
        {
            return await Loader.Where(value).FirstOrDefaultAsync();
        }

        public Task DeleteAsync(T entity)
        {
            Delete(entity);
            return Task.CompletedTask;
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> value)
        {
            return Loader.Where(value).ToListAsync();
        }
    }
}

