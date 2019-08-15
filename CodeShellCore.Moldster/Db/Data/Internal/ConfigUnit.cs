using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Security;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Data.Internal;
using System;
using CodeShellCore.Helpers;

namespace CodeShellCore.Moldster.Db.Internal
{
    public class ConfigUnit : UnitOfWork<MoldsterContext>, IConfigUnit
    {
        public override Action<ChangeLists> OnBeforeSave => lst =>
        {
            foreach (IMoldsterModel mode in lst.Added)
            {
                if (mode.Id == 0)
                    mode.Id = Utils.GenerateID();
                mode.CreatedOn = DateTime.Now;
            }

            foreach (IMoldsterModel mode in lst.Updated)
            {
                mode.UpdatedOn = DateTime.Now;
            }
        };
        public ConfigUnit(IUserAccessor acc) : base()
        {
        }


        public IMoldsterResourceRepository ResourceRepository { get { return GetRepository<IMoldsterResourceRepository>(); } }
        public IPageCategoryRepository PageCategoryRepository { get { return GetRepository<IPageCategoryRepository>(); } }
        public IPageControlRepository PageControlRepository { get { return GetRepository<IPageControlRepository>(); } }
        public IPageRepository PageRepository { get { return GetRepository<IPageRepository>(); } }
        public IDomainRepository DomainRepository { get { return GetRepository<IDomainRepository>(); } }

        public IRepository<Control> ControlRepository { get { return GetRepositoryFor<Control>(); } }
        public IRepository<DomainEntity> DomainEntityRepository { get { return GetRepositoryFor<DomainEntity>(); } }
        public IRepository<DomainEntityProperty> EntityPropertyRepository { get { return GetRepositoryFor<DomainEntityProperty>(); } }
        public IRepository<ResourceAction> ResourceActionRepository { get { return GetRepositoryFor<ResourceAction>(); } }
        public IRepository<Tenant> TenantRepository { get { return GetRepositoryFor<Tenant>(); } }
        public IRepository<TenantApp> TenantAppRepository { get { return GetRepositoryFor<TenantApp>(); } }
        
    }
}
