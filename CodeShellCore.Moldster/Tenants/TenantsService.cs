using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Data;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantsService : DtoEntityService<Tenant, long, TenantDto, LoadOptions, TenantEditDTO, TenantDto>, ITenantService
    {
        private readonly IConfigUnit unit;

        public TenantsService(IConfigUnit unit) : base(unit)
        {
            this.unit = unit;
        }

        public override SubmitResult<TenantEditDTO> Post(TenantDto obj)
        {
            if (obj.Id == 0)
            {
                long id = unit.TenantRepository.GetMax(d => d.Id);
                obj.Id = id + 1;
            }

            if (!unit.TenantRepository.Exist(d => true))
            {
                obj.IsActive = true;
            }

            if (obj.LogoFile?.TmpPath != null)
            {
                obj.Logo = "logos/" + obj.LogoFile.Name;
            }

            var res = base.Post(obj);

            if (res.IsSuccess)
            {
                IPathsService paths = unit.ServiceProvider.GetService<IPathsService>();
                if (paths != null && obj.LogoFile?.TmpPath != null)
                {
                    var newFilePath = Path.Combine(paths.UIRoot, "wwwroot\\logos", obj.LogoFile.Name);
                    File.Move(obj.LogoFile.TmpPath, newFilePath);
                }
            }

            return res;
        }

        public override SubmitResult<TenantEditDTO> Put(TenantDto obj)
        {
            if (obj.LogoFile?.TmpPath != null)
            {
                obj.Logo = "logos/" + obj.LogoFile.Name;
            }
            var c = base.Put(obj);
            if (c.IsSuccess)
            {
                IPathsService paths = unit.ServiceProvider.GetService<IPathsService>();
                if (paths != null && obj.LogoFile?.TmpPath != null)
                {
                    var newFilePath = Path.Combine(paths.UIRoot, "wwwroot\\logos", obj.LogoFile.Name);
                    File.Move(obj.LogoFile.TmpPath, newFilePath);
                }
            }
            return c;
        }
    }
}
