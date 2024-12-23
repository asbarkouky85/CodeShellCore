using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Files;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryService : DtoEntityService<PageCategory, long, PageCategoryListDTO, PagedListRequestDto, PageCategoryDto>, IPageCategoryService
    {
        private readonly IFileHandler fileHandler;
        private readonly IPathsService conf;
        private readonly IMoldsterUnit Unit;
        IMoldsterLookupService Lookups => Unit.ServiceProvider.GetService<IMoldsterLookupService>();

        public PageCategoryService(IMoldsterUnit unit, IFileHandler fileHandler, IPathsService conf) : base(unit)
        {
            this.fileHandler = fileHandler;
            this.conf = conf;
            Unit = unit;
        }

        protected override Task<PageCategory> GetSingleById(long id)
        {
            return base.GetSingleById(id);
        }

        public override async Task<PageCategoryDto> GetSingle(long id)
        {
            var cat = await base.GetSingle(id);
            if (cat != null)
            {
                cat.PageCategoryParameters = await Unit.PageCategoryParameterRepository.FindAndMap<PageCategoryParameterDto>(d => d.PageCategoryId.Equals(id));
                cat.Controls = await Unit.ControlRepository.FindAndMap<ControlDto>(d => d.PageCategoryId.Equals(id));
            }
            return cat;
        }

        public override async Task<EntitySubmitResult<PageCategoryDto>> Post(PageCategoryDto dto)
        {
            EntitySubmitResult<PageCategoryDto> returned = new EntitySubmitResult<PageCategoryDto>();

            if (string.IsNullOrEmpty(dto.Name))
                dto.Name = dto.ViewPath?.GetAfterLast("/");

            var domain = await Unit.DomainRepository.GetOrCreatePath(dto.ViewPath.GetBeforeLast("/"));

            string template = Path.Combine(Shell.AppRootPath, "Views", dto.ViewPath + ".cshtml");
            if (!fileHandler.Exists(template))
                throw new Exception("No such template : " + template);

            var mapped = Mapper.Map<PageCategoryDto, PageCategory>(dto);
            domain.PageCategories.Add(mapped);
            dto.DomainId = domain.Id;
            if (dto.ResourceName != null)
            {
                string[] sp = dto.ResourceName.Split('/');
                string res = dto.ResourceName;
                string service = null;

                if (sp.Length > 1)
                {
                    res = sp[1];
                    service = sp[0];
                }

                Resource r = await Unit.ResourceRepository.GetResource(res, service);

                string[] bases = new[] { "Edit", "List", "Tree" };
                if (bases.Contains(dto.BaseComponent) && r == null)
                {
                    throw new Exception("This " + dto.BaseComponent + " base component requires a Resource");
                }
                r.PageCategories.Add(mapped);

                returned = (await Unit.SaveChanges()).ToSubmitResult<PageCategoryDto>();
                returned.Result = await GetSingle(mapped.Id);
            }
            else
            {
                returned = await base.Post(dto);
            }
            return returned;

        }

        public Task<PagedResult<PageCategoryListDTO>> GetAll(PagedListRequestDto opt)
        {
            var opts = opt.GetOptionsFor<PageCategoryListDTO>();
            return Unit.PageCategoryRepository.FindAndMap(opts);
        }

        public async Task<PagedResult<PageCategoryListDTO>> GetPagesCategoryByDomain(long domainId, PagedListRequestDto opt)
        {
            return await Unit.PageCategoryRepository.GetUnderDomain<PageCategoryListDTO>(domainId, Mapper.Map(opt, new PagedListRequest()));
        }

        public async Task<List<TemplateDTO>> GetTemplates()
        {
            string configPath = conf.ConfigRoot;
            var DbTemplateList = await Unit.PageCategoryRepository.GetValues(d => d.ViewPath);
            return await GetLocalTemplate(DbTemplateList);
        }

        public Task<List<TemplateDTO>> GetLocalTemplate(IEnumerable<string> files)
        {
            return Task.Run(() =>
            {

                var configPath = Path.Combine(conf.ConfigRoot, "Views");
                List<TemplateDTO> templateList = new List<TemplateDTO>();
                var templates = Directory.GetFiles(configPath, "*.cshtml", SearchOption.AllDirectories);

                foreach (var temp in templates)
                {
                    var vPath = temp.Replace(configPath + "\\", "").Replace("\\", "/").Replace(".cshtml", "");
                    var name = vPath.GetAfterLast("/");
                    if (!files.Any(d => d == vPath) && name[0] != '_')
                    {
                        templateList.Add(new TemplateDTO
                        {
                            Name = name,
                            ViewPath = vPath,
                            CreatedOn = File.GetCreationTime(temp),
                            ResourceId = null,
                            BaseComponent = null
                        });
                    }

                };

                return templateList.OrderByDescending(d => d.CreatedOn).ToList();
            });
        }

        public async Task<SubmitResult> Create(List<PageCategoryDto> list)
        {
            List<Domain> doms = new List<Domain>();
            foreach (var item in list)
            {

                var d = await Unit.DomainRepository.GetOrCreatePath(item.ViewPath.GetBeforeLast("/"), doms);
                var cat = Mapper.Map<PageCategoryDto, PageCategory>(item);
                Unit.PageCategoryRepository.Add(cat, d);
            }
            return await DefaultUnit.SaveChanges();
        }

        public override async Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data)
        {
            return await Lookups.PageCategoryEdit(data);
        }

    }
}
