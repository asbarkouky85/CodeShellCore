﻿using System;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using CodeShellCore.Security;
using CodeShellCore.Helpers;
using System.Collections.Generic;
using CodeShellCore.Text;
using System.Reflection;

namespace CodeShellCore.Data
{
    public class DefaultUnitOfWork : IUnitOfWork
    {
        
        protected InstanceStore<IRepository> Store;
        protected IServiceProvider _provider;
        private ILocaleTextProvider _textProvider;
        private IUserAccessor _currentUser;
        private ClientData _clientData;

        protected virtual string EntitiesAssembly { get; set; }
        protected virtual string EntitiesNameSpace => EntitiesAssembly;

        protected virtual Dictionary<string, Type> ResourceToModel { get; }
        static Dictionary<string, Type> _resourceDictionary;

        /// <summary>
        /// Should return a type that inherits from <see cref="Repository{T, TContext}"/>
        /// </summary>
        protected virtual Type GenericRepositoryType { get; }

        /// <summary>
        /// Should return a type that implements from <see cref="ICollectionEFRepository{T, TContext}"/>
        /// </summary>
        protected virtual Type GenericCollectionRepositoryType { get; }
        public bool IsDisposed { get; protected set; } = false;

        public ILocaleTextProvider Strings
        {
            get
            {
                if (_textProvider == null)
                    _textProvider = _provider.GetService<ILocaleTextProvider>();
                return _textProvider;
            }
        }

        public IUserAccessor UserAccessor
        {
            get
            {
                if (_currentUser == null)
                    _currentUser = _provider.GetService<IUserAccessor>();
                return _currentUser;
            }
        }

        public ClientData ClientData
        {
            get
            {
                if (_clientData == null)
                    _clientData = _provider.GetService<ClientData>();
                return _clientData;
            }
        }

        public bool IgnorePermissions { get; set; }

        public IServiceProvider ServiceProvider => _provider;

        public DefaultUnitOfWork(IServiceProvider prov)
        {
            _provider = prov;
            Store = new InstanceStore<IRepository>(_provider);
            EntitiesAssembly = GetType().Assembly.GetName().Name;
        }

        public virtual event EventHandler<ChangeLists> Saving;

        public virtual void Dispose()
        {
            Store.Clear();
        }

        public virtual void EnableJsonLoading()
        {

        }

        public virtual ChangeLists GetChangeSet()
        {
            return new ChangeLists();
        }

        public virtual IRepository GetRepositoryFor(Type i)
        {
            var req = typeof(IRepository<>).MakeGenericType(i);
            var repo = _provider.GetService(req) as IRepository;
            if (repo != null)
                return repo;
            var gen = GenericRepositoryType ?? typeof(DefaultRepository<>);
            var t = gen.MakeGenericType(i);
            return Store.GetInstance(t);
        }

        protected virtual string EntityToResource(string t)
        {
            var r = t.Plurize();
            return r;
        }

        protected virtual Type ResourceToEntity(string res)
        {
            var name = res.UCFirst();
            if (_resourceDictionary == null)
            {
                _resourceDictionary = ResourceToModel ?? new Dictionary<string, Type>();
            }
            if (_resourceDictionary.TryGetValue(name, out Type t))
            {
                return t;
            }
            else
            {
                var ent = EntitiesNameSpace + "." + name.Singularize();
                var tt = Assembly.Load(EntitiesAssembly).GetType(ent);
                return tt;
            }
        }

        public IRepository GetRepository(Type t)
        {
            return Store.GetInstance(t);
        }

        public virtual ICollectionRepository<T> GetCollectionRepositoryFor<T>() where T : class
        {
            return Store.GetInstance<ICollectionRepository<T>>();
        }

        public virtual IRepository<T> GetRepositoryFor<T>() where T : class
        {
            var i = typeof(T);
            var repo = _provider.GetService<IRepository<T>>();
            if (repo != null)
                return repo;
            if (GenericRepositoryType != null)
            {
                var t = GenericRepositoryType.MakeGenericType(typeof(T));
                return (IRepository<T>)Store.GetInstance(t);
            }
            return Store.GetInstance<IRepository<T>>();
        }


        public virtual SubmitResult SaveChanges(string successMessage = null, string faileMessage = null)
        {
            Saving?.Invoke(this, new ChangeLists());
            return new SubmitResult();
        }

        public virtual T GetRepository<T>() where T : class, IRepository
        {
            return Store.GetInstance<T>();
        }

        public string TranslateIfMobile(string message_code, params string[] parameters)
        {
            if (ClientData.IsMobile)
                return Strings.Message(message_code, parameters);
            return message_code;
        }
    }
}
