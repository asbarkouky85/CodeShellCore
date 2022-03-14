using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantsService : DtoEntityService<Tenant, long, TenantDto, LoadOptions, TenantEditDTO, TenantDto>, ITenantService
    {
        private readonly IConfigUnit unit;
        private readonly INamingConventionService naming;

        public TenantsService(IConfigUnit unit, INamingConventionService naming) : base(unit)
        {
            this.unit = unit;
            this.naming = naming;
        }

        public override SubmitResult<TenantEditDTO> Post(TenantDto dto)
        {
            if (dto.Id == 0)
            {
                long id = unit.TenantRepository.GetMax(d => d.Id);
                dto.Id = id + 1;
            }

            if (!unit.TenantRepository.Exist(d => true))
            {
                dto.IsActive = true;
            }

            return base.Post(dto);
        }

        protected override void AfterCreate(TenantDto dto, Tenant entity)
        {
            SaveLogo(dto);
        }

        protected override void AfterUpdate(TenantDto dto, Tenant entity)
        {
            SaveLogo(dto);
        }

        private void SaveLogo(TenantDto dto)
        {
            IPathsService paths = unit.ServiceProvider.GetService<IPathsService>();
            if (paths != null && dto.LogoFile?.TmpPath != null)
            {
                var newFilePath = naming.GetLogoFilePath(dto.Code, dto.LogoFile.Name);
                File.Move(dto.LogoFile.TmpPath, newFilePath);
            }
        }
    }
}
