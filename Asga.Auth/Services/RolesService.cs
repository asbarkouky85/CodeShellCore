using Asga.Data;
using Asga.Security;
using Asga.Services;
using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using System.Linq;

namespace Asga.Auth.Services
{
    public class RolesService : AsgaEntityService<Role>
    {
        private readonly CurrentTenant _currentTenant;

        public RolesService(AuthUnit unit, CurrentTenant currentTenant) : base(unit)
        {
            _currentTenant = currentTenant;
        }

        private AuthUnit Unit => (AuthUnit)UnitOfWork;

        public override IRepository<Role> Repository => Unit.RoleRepository;

        public override Role GetSingle(object id)
        {
            Unit.EnableJsonLoading();

            var m = Repository.FindSingle(id);
            m.RoleResources = Unit.RoleResourceRepository.Find(d => d.RoleId.Equals(id));
            m.RoleResourceActions = Unit.RoleResourceActionRepository.Find(d => d.RoleId.Equals(id));
            m.RoleResources.ForEach(d => d.Role = null);
            m.RoleResourceActions.ForEach(d => d.Role = null);
            return m;
        }

        public override SubmitResult Update(Role obj)
        {
            //var roleInDb = Repository.Exist(x => x.Name == obj.Name.Trim() && x.Id != obj.Id);
            //if (roleInDb)
            //{
            //    return new SubmitResult(400, "Role Already Exist");


            //}
            Repository.Update(obj);
            Unit.RoleResourceRepository.ApplyChanges(obj.RoleResources);
            Unit.RoleResourceActionRepository.ApplyChanges(obj.RoleResourceActions);
            var res = Unit.SaveChanges();
            return res;
        }

        public override SubmitResult Create(Role obj)
        {
            var roleInDb = Repository.Find(x => x.Name == obj.Name.Trim());
            if (roleInDb.Count > 0)
            {
                return new SubmitResult(400, "Role Already Exist");
            }
            obj.RoleResourceActions.ForEach(i => i.Id = Utils.GenerateID());
            obj.RoleResources.ForEach(d => d.Id = Utils.GenerateID());
            return base.Create(obj);
        }

        public RoleCacheResponse GetUpdatedRoles(TenantCacheDto tenantCacheDto)
        {
            var updatedRoles = Repository.FindAs(RoleCacheResponse.Expression, d => d.UpdatedOn > tenantCacheDto.LastUpdate || d.CreatedOn > tenantCacheDto.LastUpdate);

            return new RoleCacheResponse()
            {
                LastUpdate = Unit.RoleRepository.GetLastUpdateDate(),
                RoleCacheDtos = updatedRoles
            };
        }


        public override EditingDTO<Role> GetSingleEditingDTO(object id)
        {
            var res = base.GetSingleEditingDTO(id);
            res.Entity.RoleResourceActions = Unit.RoleResourceActionRepository.Find(a => a.RoleId == (long)id);
            res.Entity.RoleResourceActions.ForEach(a => a.Role = null);

            res.Entity.RoleResources = Unit.RoleResourceRepository.Find(a => a.RoleId == (long)id);
            res.Entity.RoleResources.ForEach(a => a.Role = null);

            return res;
        }

    }
}
