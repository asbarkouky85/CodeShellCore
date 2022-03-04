using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace CodeShellCore.Moldster.Configurator.Services
{
    public class TenantsService : EntityService<Tenant>, ITenantService
    {
        private readonly IConfigUnit unit;

        public TenantsService(IConfigUnit unit) : base(unit)
        {
            this.unit = unit;
        }

        public override Tenant GetSingle(object id)
        {
            var t = base.GetSingle(id);
            t.LogoFile = new Files.TmpFileData(t.Logo);
            return t;
        }

        public override SubmitResult Create(Tenant obj)
        {
            if (obj.Id == 0)
            {
                long id = unit.TenantRepository.GetMax(d => d.Id);
                obj.Id = id + 1;
            }
            if (obj.LogoFile?.TmpPath != null)
            {
                obj.Logo = "logos/" + obj.LogoFile.Name;
            }
            if (!unit.TenantRepository.Exist(d => true))
            {
                obj.IsActive = true;
            }
            var c = base.Create(obj);
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

        public override SubmitResult Update(Tenant obj)
        {
            if (obj.LogoFile?.TmpPath != null)
            {
                obj.Logo = "logos/" + obj.LogoFile.Name;
            }
            var c = base.Update(obj);
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

        public TenantEditDTO GetSingleDTO(long id)
        {

            return unit.TenantRepository.FindSingleAs(d => new TenantEditDTO { Entity = d, Id = d.Id }, id);
        }
    }
}
