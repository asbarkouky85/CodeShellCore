using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Files.Uploads;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.CodeGeneration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantsService : DtoEntityService<Tenant, long, TenantDto, LoadOptions, TenantEditDTO, TenantDto>, ITenantService
    {
        private readonly IConfigUnit unit;
        private readonly INamingConventionService naming;
        private readonly IUploadedFilesHandler uploaded;

        public TenantsService(
            IConfigUnit unit,
            INamingConventionService naming,
            IUploadedFilesHandler uploaded) : base(unit)
        {
            this.unit = unit;
            this.naming = naming;
            this.uploaded = uploaded;
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

        protected virtual void SaveLogo(TenantDto dto)
        {
            IPathsService paths = unit.ServiceProvider.GetService<IPathsService>();
            if (paths != null && dto.LogoFile?.TmpPath != null)
            {
                var newFilePath = naming.GetLogoFilePath(dto.Code, dto.LogoFile.Name);
                var path = Path.Combine(uploaded.TempRoot, dto.LogoFile.TmpPath);
                Utils.CreateFolderForFile(newFilePath);
                File.Move(path, newFilePath);
            }
        }
    }
}
