using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Files.Uploads;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.CodeGeneration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantsService : DtoEntityService<Tenant, long, TenantDto, PagedListRequestDto, TenantEditDTO, TenantDto>, ITenantService
    {
        private readonly IMoldsterUnit unit;
        private readonly INamingConventionService naming;
        private readonly IUploadedFilesHandler uploaded;

        public TenantsService(
            IMoldsterUnit unit,
            INamingConventionService naming,
            IUploadedFilesHandler uploaded) : base(unit)
        {
            this.unit = unit;
            this.naming = naming;
            this.uploaded = uploaded;
        }

        public override async Task<EntitySubmitResult<TenantEditDTO>> Post(TenantDto dto)
        {
            var entity = Mapper.Map<TenantDto, Tenant>(dto);

            long id = await unit.TenantRepository.GetMax(d => d.Id);
            entity.Id = id + 1;
            if (!(await unit.TenantRepository.Exist(d => true)))
            {
                entity.IsActive = true;
            }
            Repository.Add(entity);
            var res = (await unit.SaveChanges()).MapToResult<EntitySubmitResult<TenantEditDTO>>();
            if (res.IsSuccess)
            {
                await AfterCreate(dto, entity);
                res.Result = await GetSingle(entity.Id);
            }
            return res;
        }

        protected override async Task AfterCreate(TenantDto dto, Tenant entity)
        {
            await SaveLogo(dto);
        }

        protected override async Task AfterUpdate(TenantDto dto, Tenant entity)
        {
            await SaveLogo(dto);
        }

        protected virtual Task SaveLogo(TenantDto dto)
        {
            return Task.Run(() =>
            {
                IPathsService paths = unit.ServiceProvider.GetService<IPathsService>();
                if (paths != null && dto.LogoFile?.FileTempPath != null)
                {
                    var newFilePath = naming.GetLogoFilePath(dto.Code, dto.LogoFile.FileName);
                    var path = Path.Combine(uploaded.TempRoot, dto.LogoFile.FileTempPath);
                    Utils.CreateFolderForFile(newFilePath);
                    File.Move(path, newFilePath);
                }
            });
        }
    }
}
